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
    private float _playerInitialPosition;
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
    
    private UnityArmatureComponent _playerAnimation;
    private bool _accelerationAnimationPlayed = false;
    private bool _decelerationAnimationPlayed = false;
    
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
        Deceleration
    }
    
    void Start()
    {
        LoadComponents();
        SetInitialVariables();
    }

    void Update()
    {
        HanldeShooting();
        HandleJump();
        HandleMovement();
        Animate();
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
            playerRigidBody.AddForce(new Vector2( backwardJumpHorizontalForce * jumpHeight, backwardJumpHeightFactor * jumpHeight) , ForceMode2D.Impulse);
        } else if (IsNormalJump())
        {
            SwitchAnimation(PlayerAnimation.Idle);
            _jumpKind = JumpKind.Normal;
            playerRigidBody.AddForce(new Vector2(normalJumpHorizontalForce * jumpHeight, normalJumpHeightFactor * jumpHeight) , ForceMode2D.Impulse);

        } else if (IsBigJump())
        {
            SwitchAnimation(PlayerAnimation.Acceleration);
            _jumpKind = JumpKind.Forward;
           playerRigidBody.AddForce(new Vector2( forwardJumpHorizontalForce * jumpHeight, forwardJumpHeightFactor * jumpHeight) , ForceMode2D.Impulse);
        }
    }
    
    private void HandleJumpMotionAboveGround()
    {
        if (_jumpKind == JumpKind.Backward && ShouldPlayerSlowDown(true))
        {
            SlowDown();
        } else if (_jumpKind == JumpKind.Forward && ShouldPlayerAccelerate(true))
        {
            Accelerate();
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    private void HandleMovement()
    {


        if (ShouldPlayerAccelerate())
        {

            Accelerate();
            _jumpKind = JumpKind.Forward;
        }  else if (ShouldPlayerSlowDown())
        { 
            SlowDown(); 
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
            NormalizeSpeed();
        }        
        


    }

    private void Accelerate()
    {
        SwitchAnimation(PlayerAnimation.Acceleration);
        transform.Translate(Vector3.right * playerAccelerationSpeed * Time.deltaTime);
        GameManager.Instance.SetFastPlayerSpeed(); 
    }

    private void SlowDown()
    {
        SwitchAnimation(PlayerAnimation.Deceleration);
        transform.Translate(Vector3.left * playerSlowDownSpeed * Time.deltaTime);
        GameManager.Instance.SetSlowPlayerSpeed();  
    }



    private void HanldeShooting()
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
        Instantiate(verticalProjectile, transform.position, verticalProjectile.transform.rotation);
    }

    private void CreateHorizontalProjectile()
    {
        Instantiate(horizontalProjectile, transform.position, horizontalProjectile.transform.rotation);
    }

    private bool ShouldPlayerAccelerate(bool ignoreKey = false)
    {
        return ((Input.GetKey(KeyCode.RightArrow) || ignoreKey) && transform.position.x < _playerInitialPosition + maxPlayerDistanceRightDirection && !IsPlayerAboveGround());
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
            GameManager.Instance.SetNormalPlayerSpeed();
            _jumpKind = JumpKind.Normal;
            SwitchAnimation(PlayerAnimation.Idle);

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
        //return Input.GetKey(KeyCode.LeftArrow);
        return _jumpKind == JumpKind.Backward;
    }

    private bool IsNormalJump()
    {
        //return !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
        return _jumpKind == JumpKind.Normal;
    }

    private bool IsBigJump()
    {
        //return Input.GetKey(KeyCode.RightArrow);
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
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.Instance.ResetScore();
            SceneManager.LoadScene("SampleScene");

        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            GameManager.Instance.ResetScore();
            SceneManager.LoadScene("SampleScene");
        }

    }

    private void PlayAccelerateAnimation()
    {

            _decelerationAnimationPlayed = false;
            if (!_accelerationAnimationPlayed)
            {
                _playerAnimation.animation.Stop();
                _playerAnimation.animation.Play("drive_acceleration",  1);
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
        }
    }

}
