using System;
using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{

    public int pointsForKill;

    public GameObject bomb;
    
    public float minWaitBeforeBombDrop;
    public float maxWaitBeforeBombDrop;

    public float waitBeforeEscape;
    private bool _escapeEnabled;
    private EscapeDirection _escapeDirection;
    public EnterDirection _enterDirection;

    public float enemySpeed;
    public float enemyEscapingSpeed;
 
    private bool _isBombSpawning;

    private GameObject _enemyArea;
    private BoxCollider2D _enemyAreaBoxCollider;
    private BoxCollider2D _enemyBoxCollider;
    private Bounds _bounds;

    private float _leftBound;
    private float _rightBound;
    private float _topBound;
    private float _bottomBound;
    private Vector3 _targetPosition;
    private bool _isTargetPositionReached = true;
    private bool _customMovingEnabled = false;

    private string _enemyStackID;

    private UnityArmatureComponent _enemyAnimation;

    private EnemyStatus _enemyStatus = EnemyStatus.Alive;
    
    private enum EscapeDirection
    {
        Left,
        Top,
        Right
    }
    
    public enum EnterDirection
    {
        Left,
        Top,
        Right
    }

    public enum EnemyStatus
    {
        Alive,
        Dying,
        Destroyed,
        Killed
    }
 
    void Awake()
    {
        _isBombSpawning = false;
    }

    void Start()
    {
        LoadComponents();
        CalculateEnemyAreaBounds();
        Invoke(methodName:"EnableEscaping", waitBeforeEscape);
    }

    void Update()
    {
        HandleSpawningBomb();
        
        if (_enemyStatus == EnemyStatus.Alive)
        {
            HandleMovingEnemy();
        } else if (_enemyStatus == EnemyStatus.Dying)
        {
            if (_enemyAnimation.animation.isCompleted)
            {
                _enemyStatus = EnemyStatus.Killed;
                Kill();
            }
        }
    }
    
    private void LoadComponents()
    {
        _enemyArea = GameObject.FindGameObjectsWithTag("EnemyArea")[0];
        _enemyAreaBoxCollider = _enemyArea.GetComponent<BoxCollider2D>();
        _bounds = _enemyAreaBoxCollider.bounds;
        _enemyAnimation = GetComponent<UnityArmatureComponent>();
        _enemyBoxCollider = GetComponent<BoxCollider2D>();
    }
    
    private void CalculateEnemyAreaBounds()
    {
        _leftBound = _bounds.min.x;
        _rightBound =  _bounds.max.x;
        _topBound = _bounds.max.y;
        _bottomBound = _bounds.min.y;

    }
    
    private void EnableEscaping()
    {
        GenerateEscapeDirection();
        _escapeEnabled = true;
    }

    private void GenerateEscapeDirection()
    {
        Array values = Enum.GetValues(typeof(EscapeDirection));
        Random random = new Random();
        EscapeDirection randomDirection = (EscapeDirection)values.GetValue(random.Next(values.Length));
        _escapeDirection = randomDirection;
    }
    
    private void HandleSpawningBomb()
    {
        if (!_isBombSpawning)
        {
            float timer = UnityEngine.Random.Range(minWaitBeforeBombDrop, maxWaitBeforeBombDrop);
            Invoke("SpawnBomb", timer);
            _isBombSpawning = true;
        }  
    }
 
    void SpawnBomb()
    {
        _enemyAnimation.animation.Stop();
        _enemyAnimation.animation.Play("Enemy_Shot", 1);
        Instantiate(bomb, transform.position, transform.rotation);
        _isBombSpawning = false;
    }
    

    private void HandleMovingEnemy()
    {
        if (_enemyAnimation.animation.isCompleted)
        {
            _enemyAnimation.animation.Play("Enemy_Fly", 1);
        }
        
        if (_escapeEnabled)
        {
            EscapeMoving();
        } 
        else if (_customMovingEnabled)
        {
            CustomMoving();
        }
        else
        {
            StandbyMoving();
        }


    }

    private void EscapeMoving()
    {
        if (_escapeDirection == EscapeDirection.Left)
        {
            transform.Translate(Vector3.left * enemyEscapingSpeed * Time.deltaTime);
        } else if (_escapeDirection == EscapeDirection.Right)
        {
            transform.Translate(Vector3.right * enemyEscapingSpeed * Time.deltaTime);

        } else if (_escapeDirection == EscapeDirection.Top)
        {
            transform.Translate(Vector3.up * enemyEscapingSpeed * Time.deltaTime);
        }
    }

    private void CustomMoving()
    {
        SetIsTargetPositionReached();

        if (!_isTargetPositionReached)
        {
            MoveTowardsTargetPosition();
        }
        else
        {
            HandleGeneratingNewPosition();
        }
    }

    private void SetIsTargetPositionReached()
    {
        if (EnemyReachedTargetPosition())
        {
            _isTargetPositionReached = true;
        }
    }

    private bool EnemyReachedTargetPosition()
    {
        return transform.position == _targetPosition;
    }

    private void MoveTowardsTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, enemySpeed * Time.deltaTime);
    }

    private void HandleGeneratingNewPosition()
    {
        _targetPosition = GenerateNewPosition();
        _isTargetPositionReached = false;
    }
    
    private Vector3 GenerateNewPosition()
    {
        return new Vector3(GenerateNewXPosition(), GenerateNewYPosition(), 0);
    }
    
    private float GenerateNewXPosition()
    {
        return UnityEngine.Random.Range(_leftBound, _rightBound);
    }

    private float GenerateNewYPosition()
    {
        return UnityEngine.Random.Range(_topBound, _bottomBound);
    }

    private void StandbyMoving()
    {
        if (_enterDirection == EnterDirection.Left)
        {
            if (EnemyReachedLeftSide())
            {
                EnableCustomMoving();
            }
            else
            {
                MoveTowardsLeftSide();
            }
        } else if (_enterDirection == EnterDirection.Top)
        {
            if (EnemyReachedTopSide())
            {
                EnableCustomMoving();
            }
            else
            {
                MoveTowardsTopSide();
            }
        } else if (_enterDirection == EnterDirection.Right)
        {
            if (EnemyReachedRightSide())
            {
                EnableCustomMoving();
            }
            else
            {
                MoveTowardsRightSide();
            }
        }
    }

    private bool EnemyReachedLeftSide()
    {
        return transform.position.x >= _bounds.center.x - (_bounds.extents.x * 0.7);
    }

    private bool EnemyReachedTopSide()
    {
        return transform.position.y <= _bounds.center.y + (_bounds.extents.y * 0.7);
    }

    private bool EnemyReachedRightSide()
    {
        return transform.position.x <= _bounds.center.x + (_bounds.extents.x * 0.7);
    }

    private void MoveTowardsLeftSide()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_rightBound, transform.position.y, transform.position.z), enemySpeed * Time.deltaTime);
    }

    private void MoveTowardsTopSide()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _bottomBound, transform.position.z), enemySpeed * Time.deltaTime);
    }
    
    private void MoveTowardsRightSide()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_leftBound, transform.position.y, transform.position.z), enemySpeed * Time.deltaTime);
    }
    private void EnableCustomMoving()
    {
        _customMovingEnabled = true;
    }


    private void HandleCollisionWithProjectile(Collider2D collision)
    {
        _enemyBoxCollider.enabled = false;
        _enemyStatus = EnemyStatus.Dying;
        Destroy(collision.gameObject);
        PlayDeathAnimation();
    }
    
    public void Kill()
    {
        Destroy(gameObject);
        AddPointsForKill();
    }

    private void PlayDeathAnimation()
    {
        _enemyAnimation.animation.Play("Enemy_Death", 1);
    }

    private void AddPointsForKill()
    {
        GameManager.Instance.AddPoints(pointsForKill);
        GameObject.Find("SpawnManager").GetComponent<BonusPoints>().DecreaseStackByOne(_enemyStackID);
    }

    public void Destroy()
    {
        Destroy(gameObject);
        GameObject.Find("SpawnManager").GetComponent<BonusPoints>().RemoveStack(_enemyStackID);
    }
    
    

    public void SetEnemyStackID(string id)
    {
        _enemyStackID = id;
    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
            HandleCollisionWithProjectile(collision);
        }

    }
}
