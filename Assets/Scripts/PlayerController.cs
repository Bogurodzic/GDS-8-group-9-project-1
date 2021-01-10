﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DragonBones;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player speed")] 
    public float playerAccelerationSpeed;
    public float playerSlowDownSpeed;
    public float maxPlayerDistanceLeftDirection;
    public float maxPlayerDistanceRightDirection;
    public float normalizationSpeedDelay;
    private float _playerInitialPosition;
    private bool _startedSpeedNormalization = false;
    private bool _speedNormalizationActive = false;
    private bool _accelerationInitialized = false;
    private float _currentPlayerAccelerationSpeed;
    private bool _decelerationInitialized = false;
    private float _currentPlayerDecelerationSpeed; 
    [Space(10)]
    
    [Header("Jump")]
    public float jumpHeight;
    public float backwardJumpHorizontalForce;
    public float backwardJumpHeightFactor;
    public float normalJumpHorizontalForce ;
    public float normalJumpHeightFactor;
    public float forwardJumpHorizontalForce;
    public float forwardJumpHeightFactor;
    private bool _jumpReady = true;
    private bool _isJumping = false;
    private JumpKind _jumpKind = JumpKind.Normal;
    [Space(10)]

    [Header("Shooting")]
    public GameObject horizontalProjectile;
    public GameObject verticalProjectile;
    public int horizontalProjectileInterval;
    public int verticalProjectileInterval;
    private bool _horizontalShootReady = true;
    private bool _verticalShootReady = true;
    [Space(10)]
    
    private Rigidbody2D playerRigidBody;

    private bool _deathInitialized = false;
    private bool _finalDeathPositionReached = false;
    private float _finalDeathPosition;
    
    private UnityArmatureComponent _playerAnimation;
    private bool _accelerationAnimationPlayed = false;
    private bool _decelerationAnimationPlayed = false;
    private bool _deathAnimationPlayed = false;

    
    private enum JumpKind
    {
        Backward,
        Normal,
        Forward
    }

    private PlayerAnimation _animationToDisplay;
    private enum PlayerAnimation
    {
        Idle,
        Acceleration,
        Deceleration,
        Death,
        None
    }
    
    void Start()
    {
        LoadComponents();
        SetInitialVariables();
    }

    void Update()
    {
        if (IsGameRunning())
        {
            HandleShooting();
            HandleJump();
            HandleMovement();
            Animate();
        } else if (!IsGameRunning())
        {
            HandleDeathMovement();
        }
    }

    private void HandleDeathMovement()
    {
        if (ShouldPlayerMoveTowardsFinalDeathPosition())
        {
            if (FinalDeathPositionIsOnRight())
            {
                transform.Translate(Vector3.right * (playerAccelerationSpeed * 1.5f) * Time.deltaTime);
            } else if (FinalDeathPositionIsOnLeft())
            {
                transform.Translate(Vector3.left * (playerAccelerationSpeed * 1.5f) * Time.deltaTime);
            }
        } else if (IsFinalDeathPositionReached())
        {
            Animate();
            if (AnimationReadyToPlay())
            {
                HandleLogicAfterDeath();
            }
        }
    }

    private bool ShouldPlayerMoveTowardsFinalDeathPosition()
    {
        return _deathInitialized && !_finalDeathPositionReached;
    }

    private bool FinalDeathPositionIsOnRight()
    {
        return transform.position.x < _finalDeathPosition;
    }

    private bool FinalDeathPositionIsOnLeft()
    {
        return transform.position.x > _finalDeathPosition;
    }

    private bool IsFinalDeathPositionReached()
    {
        return _finalDeathPositionReached;
    }
    

    
    private void LoadComponents()  
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<UnityArmatureComponent>();
    }

    private void SetInitialVariables()
    {
        _playerInitialPosition = transform.position.x;
    }
    
    private bool IsPlayerAboveGround()
    {
        return _isJumping;
    }
    
    private void HandleJump()
    {
        if (ShouldPlayerJump())
        {
            HandleJumpMotionOnGround();
        } else if (IsPlayerAboveGround())
        {
            HandleConstraints();
        }
    }

    private void HandleJumpMotionOnGround()
    {
        playerRigidBody.freezeRotation = true;
        _jumpReady = false;
        _isJumping = true;
        
        if (IsBackJump())
        {
            SwitchAnimation(PlayerAnimation.Deceleration);
            _jumpKind = JumpKind.Backward;
            if (PlayerAheadInitialPosition())
            {
                playerRigidBody.AddForce(new Vector2( backwardJumpHorizontalForce * jumpHeight * 1.1f, backwardJumpHeightFactor * jumpHeight * 1.1f) , ForceMode2D.Force);
            } else if (PlayerBehindInitialPosition())
            {
                playerRigidBody.AddForce(new Vector2( backwardJumpHorizontalForce * jumpHeight, backwardJumpHeightFactor * jumpHeight) , ForceMode2D.Force);

            }
        } else if (IsNormalJump())
        {
            SwitchAnimation(PlayerAnimation.Idle);
            _jumpKind = JumpKind.Normal;
            playerRigidBody.AddForce(new Vector2(normalJumpHorizontalForce * jumpHeight, normalJumpHeightFactor * jumpHeight) , ForceMode2D.Force);

        } else if (IsBigJump())
        {
            SwitchAnimation(PlayerAnimation.Acceleration);
            _jumpKind = JumpKind.Forward;
            playerRigidBody.AddForce(new Vector2( forwardJumpHorizontalForce * jumpHeight, forwardJumpHeightFactor * jumpHeight) , ForceMode2D.Force);
        }
    }
    
    private void HandleMovement()
    {
        if (ShouldPlayerAccelerate())
        {
            Accelerate();
            ResetNormalizationStates();
            _jumpKind = JumpKind.Forward;
        }  else if (ShouldPlayerSlowDown())
        { 
            SlowDown();
            ResetNormalizationStates();
            _jumpKind = JumpKind.Backward;
        } else if (ShouldPlayerMaintainSpeed())
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                SwitchAnimation(PlayerAnimation.Acceleration);
            } else if (Input.GetKey(KeyCode.LeftArrow))
            {
                SwitchAnimation(PlayerAnimation.Deceleration);
            }
        }
        else
        {
            HandleNormalizeSpeed();
        }
    }

    private void Accelerate()
    {
        SwitchAnimation(PlayerAnimation.Acceleration);
        GameManager.Instance.SetFastPlayerSpeed();

        _decelerationInitialized = false;
        
        if (_accelerationInitialized == false)
        {
            _accelerationInitialized = true;
            _currentPlayerAccelerationSpeed = 0;
        }
        else
        {
            if (_currentPlayerAccelerationSpeed < playerAccelerationSpeed)
            {
                _currentPlayerAccelerationSpeed += 0.0065f;
            }
            transform.Translate(Vector3.right * _currentPlayerAccelerationSpeed * Time.deltaTime);
        }
    }

    private void SlowDown()
    {
        SwitchAnimation(PlayerAnimation.Deceleration);
        GameManager.Instance.SetSlowPlayerSpeed();

        _accelerationInitialized = false;

        if (_decelerationInitialized == false)
        {
            _decelerationInitialized = true;
            _currentPlayerDecelerationSpeed = 0;
        }
        else
        {
            if (PlayerAheadInitialPosition())
            {
                if (_currentPlayerDecelerationSpeed < playerSlowDownSpeed)
                {
                    _currentPlayerDecelerationSpeed += 0.0075f;
                }
                transform.Translate(Vector3.left * _currentPlayerDecelerationSpeed * Time.deltaTime); 
            } else if (PlayerBehindInitialPosition())
            {
                if (_currentPlayerDecelerationSpeed < playerSlowDownSpeed / 2)
                {
                    _currentPlayerDecelerationSpeed += 0.0075f;
                } else if (_currentPlayerDecelerationSpeed > playerSlowDownSpeed / 2)
                {
                    _currentPlayerDecelerationSpeed -= 0.0075f;
                }
                transform.Translate(Vector3.left * _currentPlayerDecelerationSpeed * Time.deltaTime);
            }

        }
        
    }

    private bool PlayerAheadInitialPosition()
    {
        return transform.position.x > _playerInitialPosition;
    }

    private bool PlayerBehindInitialPosition()
    {
        return transform.position.x <= _playerInitialPosition;
    }

    private void ResetNormalizationStates()
    {
        _speedNormalizationActive = false;
        _startedSpeedNormalization = false;
    }



    private void HandleShooting()
    {
        if (ShouldPlayerShootHorizontally())
        {
            CreateHorizontalProjectile();
            ReloadHorizontalShoot();
        }
        
        if (ShouldPlayerShootVertically())
        {
            CreateVerticalProjectile();
            ReloadVerticalShoot();
        }
    }

    private void ReloadHorizontalShoot()
    {
        DisableHorizontalShoot();
        Task.Delay(horizontalProjectileInterval * 1000).ContinueWith(t=> EnableHorizontalShoot());
    }
    
    private void ReloadVerticalShoot()
    {
        DisableVerticalShoot();
        Task.Delay(verticalProjectileInterval * 1000).ContinueWith(t=> EnableVerticalShoot());
    }

    private void DisableHorizontalShoot()
    {
        _horizontalShootReady = false;
    }
    private void EnableHorizontalShoot()
    {
        _horizontalShootReady = true;
    }
    
    private void DisableVerticalShoot()
    {
        _verticalShootReady = false;
    }
    private void EnableVerticalShoot()
    {
        _verticalShootReady = true;
    }

    private void CreateVerticalProjectile()
    {
        Instantiate(verticalProjectile, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.2f, transform.position.z), verticalProjectile.transform.rotation);
    }

    private void CreateHorizontalProjectile()
    {
        Instantiate(horizontalProjectile, new Vector3(transform.position.x + 1.1f, transform.position.y - 0.23f, transform.position.z), horizontalProjectile.transform.rotation);
    }

    private bool ShouldPlayerAccelerate(bool ignoreKey = false)
    {
        return ((Input.GetKey(KeyCode.RightArrow) || ignoreKey) && transform.position.x < _playerInitialPosition + maxPlayerDistanceRightDirection && !IsPlayerAboveGround());
    }

    private void HandleNormalizeSpeed()
    {
        if (!_startedSpeedNormalization)
        {
            _startedSpeedNormalization = true;
            Invoke("ActiveSpeedNormalization", normalizationSpeedDelay);
        } else if (_startedSpeedNormalization && _speedNormalizationActive)
        {
            NormalizeSpeed();
        }
    }

    private void ActiveSpeedNormalization()
    {
        _speedNormalizationActive = true;
    }

    private void NormalizeSpeed()
    {
        if (transform.position.x < _playerInitialPosition - 0.4)
        {
            Accelerate();
            _jumpKind = JumpKind.Normal;

        } 
        else if (transform.position.x > _playerInitialPosition + 0.4)
        {
            SlowDown();
            _jumpKind = JumpKind.Backward;
        }
        else
        {
            _decelerationInitialized = false;
            _accelerationInitialized = false;
            
            GameManager.Instance.SetNormalPlayerSpeed();
            _jumpKind = JumpKind.Normal;
            SwitchAnimation(PlayerAnimation.Idle);
        }
    }
    
    

    private void HandleConstraints()
    {
        if ( transform.position.x <= _playerInitialPosition - maxPlayerDistanceLeftDirection)
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        } else if (transform.position.x >= _playerInitialPosition + maxPlayerDistanceRightDirection)
        {
            playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
        }
    }


    private bool ShouldPlayerSlowDown(bool ignoreKey = false)
    {
        return ((Input.GetKey(KeyCode.LeftArrow) || ignoreKey) && transform.position.x > _playerInitialPosition - maxPlayerDistanceLeftDirection && !IsPlayerAboveGround());
    }

    private bool ShouldPlayerMaintainSpeed()
    {
        return (_isJumping || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
    }

    private bool ShouldPlayerJump()
    {
        return (Input.GetKeyDown(KeyCode.UpArrow) && _jumpReady && !_isJumping);
    }

    private bool IsBackJump()
    {
        return _jumpKind == JumpKind.Backward;
    }

    private bool IsNormalJump()
    {
        return _jumpKind == JumpKind.Normal;
    }

    private bool IsBigJump()
    {
        return _jumpKind == JumpKind.Forward;
    }

    private bool ShouldPlayerShootHorizontally(){
        return (Input.GetKeyDown(KeyCode.Space) && _horizontalShootReady);
    }
    private bool ShouldPlayerShootVertically(){
        return (Input.GetKeyDown(KeyCode.Space) && _verticalShootReady);
    }

    private void HandleReturningOnGround()
    {
        _jumpReady = true;
        _isJumping = false;
        playerRigidBody.freezeRotation = false;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            HandleReturningOnGround();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            HandleLogicAfterCollisionWithProjectile(collision);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {

            HandleLogicAfterCollisionWithHole(collision);

        }
    }



    private void StopGame()
    {
        GameManager.Instance.StopGame();
    }

    private bool IsGameRunning()
    {
        return GameManager.Instance.IsGameRunning();
    }

    private void InitializeDeath()
    {
        _deathInitialized = true;
    }

    private void SetFinalDeathPosition(float position)
    {
        _finalDeathPosition = position;
    }

    private void ReachFinalDeathPosition()
    {
        _finalDeathPositionReached = true;
        SwitchAnimation(PlayerAnimation.Death);
    }

    private void HandleLogicAfterCollisionWithProjectile(Collider2D collision)
    {
        Destroy(collision.gameObject);
        GameManager.Instance.PlayerDeath();
        SwitchAnimation(PlayerAnimation.None);
        SetFinalDeathPosition(collision.bounds.center.x);
        InitializeDeath();
        ReachFinalDeathPosition();
    }
    private void HandleLogicAfterCollisionWithHole(Collider2D collision)
    {
        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 0);
        GameManager.Instance.PlayerDeath();
        SwitchAnimation(PlayerAnimation.None);
        SetFinalDeathPosition(collision.bounds.center.x);
        InitializeDeath();
        Invoke("ReachFinalDeathPosition", 0.65f);
    }

    private void HandleLogicAfterDeath()
    {
        if (GameManager.Instance.CanRespawnPlayer())
        {
            RespawnPlayer();
        }
        else
        {
            GameManager.Instance.ResetGame();
        }

    }

    private void RespawnPlayer()
    {
        GameManager.Instance.RespawnPlayer();
        SceneManager.LoadScene("SampleScene");
        SwitchAnimation(PlayerAnimation.Idle);
        _deathInitialized = false;
        _finalDeathPositionReached = false;
        _accelerationAnimationPlayed = false;
        _deathAnimationPlayed = false;
        _decelerationAnimationPlayed = false;
        _jumpReady = true;
        _isJumping = false;
    }

    private void PlayAccelerateAnimation()
    {

            _decelerationAnimationPlayed = false;
            if (!_accelerationAnimationPlayed)
            {
                _playerAnimation.animation.Stop();
                _playerAnimation.animation.FadeIn("drive_acceleration",  0.2f, 1);
                _accelerationAnimationPlayed = true;
            }
            else
            {
                if (AnimationReadyToPlay())
                {
                    
                    _playerAnimation.animation.Play("drive_fast", 1);
                }
            }        
        
    }

    private void PlayDecelerationAnimation()
    {

            _accelerationAnimationPlayed = false;
            if (!_decelerationAnimationPlayed)
            {
                _playerAnimation.animation.Stop();
                _playerAnimation.animation.Play("drive_deceleration", 1);
                _decelerationAnimationPlayed = true;
            }
            else
            {
                if (AnimationReadyToPlay())
                {
                    _playerAnimation.animation.Play("drive_idle", 1);
                }
            }          
        
    }

    private void PlayIdleAnimation()
    {
        if (AnimationReadyToPlay())
        {
            _accelerationAnimationPlayed = false;
            _decelerationAnimationPlayed = false;
            _playerAnimation.animation.Play("drive_idle", 1);
        }
    }

    private void PlayDeathAnimation()
    {
        if (!_deathAnimationPlayed)
        {
            _deathAnimationPlayed = true;
            _playerAnimation.animation.Stop();
            _playerAnimation.animation.Play("Death_Player", 1);  
        }

    }

    private bool AnimationReadyToPlay()
    {
        return !_playerAnimation.animation.isPlaying || _playerAnimation.animation.isCompleted;
    }

    private void SwitchAnimation(PlayerAnimation animation)
    {
        _animationToDisplay = animation;
    }

    private void Animate()
    {
        switch (_animationToDisplay)
        {
            case PlayerAnimation.Acceleration:
                PlayAccelerateAnimation();
                break;
            case PlayerAnimation.Deceleration:
                PlayDecelerationAnimation();
                break;
            case PlayerAnimation.Idle:
                PlayIdleAnimation();
                break;
            case PlayerAnimation.Death:
                PlayDeathAnimation();
                break;
        }
    }
    
}
