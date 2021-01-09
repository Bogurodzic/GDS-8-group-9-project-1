using System;
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
    public float normalizeSpeedFactor;
    private float _playerInitialPosition;
    [Space(10)]
    
    [Header("Jump")]
    public float jumpHeight;
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
    
    void Start()
    {
        LoadComponents();
        SetInitialVariables();
    }

    void Update()
    {
        HanldeShooting();
        HandleJump();

        if (!AnimateJump())
        {
            HandleMovement();
        }
        else
        {
            HandleAnimatingJump();
        }

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

    private bool AnimateJump()
    {
        return _isJumping;
    }

    private void HandleAnimatingJump()
    {
        if (_jumpKind == JumpKind.Backward && ShouldPlayerSlowDown(true))
        {
            SlowDown();
        } else if (_jumpKind == JumpKind.Forward && ShouldPlayerAccelerate(true))
        {
            Accelerate();
        } 
    }

    private void HandleJump()
    {
        if (ShouldPlayerJump())
        {
            playerRigidBody.freezeRotation = true;
            _jumpReady = false;
            _isJumping = true;
            
            if (IsBackJump())
            {
                _jumpKind = JumpKind.Backward;
                playerRigidBody.AddForce(Vector3.up * (jumpHeight * 0.75f) , ForceMode2D.Impulse);
            } else if (IsNormalJump())
            {
                _jumpKind = JumpKind.Normal;
                playerRigidBody.AddForce(Vector3.up * (jumpHeight * 0.95f) , ForceMode2D.Impulse); 
            } else if (IsBigJump())
            {
                _jumpKind = JumpKind.Forward;
                playerRigidBody.AddForce(Vector3.up * (jumpHeight * 1.1f) , ForceMode2D.Impulse);             
            }

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
                PlayAccelerateAnimation();
            } else if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayDecelerationAnimation();
            }
        }
        else
        {
            NormalizeSpeed();
        }

    }

    private void Accelerate()
    {
        PlayAccelerateAnimation();
        transform.Translate(Vector3.right * playerAccelerationSpeed * Time.deltaTime);
        
        
        //playerRigidBody.AddForce(Vector3.right * playerAccelerationSpeed);

        GameManager.Instance.SetFastPlayerSpeed(); 
    }

    private void SlowDown()
    {
        PlayDecelerationAnimation();
        transform.Translate(Vector3.left * playerSlowDownSpeed * Time.deltaTime);
        //playerRigidBody.AddForce(Vector3.left * playerAccelerationSpeed);
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
        return ((Input.GetKey(KeyCode.RightArrow) || ignoreKey) && transform.position.x < _playerInitialPosition + maxPlayerDistanceRightDirection);
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
            PlayIdleAnimation();
        }
    }


    private bool ShouldPlayerSlowDown(bool ignoreKey = false)
    {
        return ((Input.GetKey(KeyCode.LeftArrow) || ignoreKey) && transform.position.x > _playerInitialPosition - maxPlayerDistanceLeftDirection);
    }

    private bool ShouldPlayerMaintainSpeed()
    {
        return (_isJumping || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow));
    }

    private bool ShouldPlayerJump()
    {
        return (Input.GetKeyDown(KeyCode.UpArrow) && _jumpReady);
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
        if (!_playerAnimation.animation.isPlaying || _playerAnimation.animation.isCompleted)
        {
            _decelerationAnimationPlayed = false;
            if (!_accelerationAnimationPlayed)
            {
                _playerAnimation.animation.Play("drive_acceleration", 1);
                _accelerationAnimationPlayed = true;
            }
            else
            {
                _playerAnimation.animation.Play("drive_fast", 1);
            }        
        }
    }

    private void PlayDecelerationAnimation()
    {
        if (!_playerAnimation.animation.isPlaying || _playerAnimation.animation.isCompleted)
        {
            _accelerationAnimationPlayed = false;
            if (!_decelerationAnimationPlayed)
            {
                _playerAnimation.animation.Play("drive_deceleration", 1);
                _decelerationAnimationPlayed = true;
            }
            else
            {
                _playerAnimation.animation.Play("drive_idle", 1);
            
            }          
        }
    }

    private void PlayIdleAnimation()
    {
        if (!_playerAnimation.animation.isPlaying || _playerAnimation.animation.isCompleted)
        {
            _accelerationAnimationPlayed = false;
            _decelerationAnimationPlayed = false;
            _playerAnimation.animation.Play("drive_idle", 1);
        }
    }

}
