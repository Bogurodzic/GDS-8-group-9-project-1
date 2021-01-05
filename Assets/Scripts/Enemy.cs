using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{

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

    private float _leftBound;
    private float _rightBound;
    private float _topBound;
    private float _bottomBound;
    private Vector3 _targetPosition;
    private bool _isTargetPositionReached = true;
    private bool _customMovingEnabled = false;

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
 
    void Awake()
    {
        _isBombSpawning = false;
    }

    void Start()
    {
        _enemyArea = GameObject.FindGameObjectsWithTag("EnemyArea")[0];
        _enemyAreaBoxCollider = _enemyArea.GetComponent<BoxCollider2D>();
        CalculateEnemyAreaBounds();
        Invoke("EnableCustomMoving", 3);
        Invoke(methodName:"EnableEscaping", waitBeforeEscape);
    }
    
    
 
    void Update()
    {
        HandleSpawningBomb();
        HandleMovingEnemy();

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
        Instantiate(bomb, transform.position, transform.rotation);
        _isBombSpawning = false;
    }

    private void CalculateEnemyAreaBounds()
    {
        _leftBound = _enemyAreaBoxCollider.bounds.min.x;
        _rightBound =  _enemyAreaBoxCollider.bounds.max.x;
        _topBound = _enemyAreaBoxCollider.bounds.max.y;
        _bottomBound = _enemyAreaBoxCollider.bounds.min.y;

    }

    private float GenerateNewXPosition()
    {
        return UnityEngine.Random.Range(_leftBound, _rightBound);
    }

    private float GenerateNewYPosition()
    {
        return UnityEngine.Random.Range(_topBound, _bottomBound);
    }

    private Vector3 GenerateNewPosition()
    {
        return new Vector3(GenerateNewXPosition(), GenerateNewYPosition(), 0);
    }

    private void HandleMovingEnemy()
    {
        
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

    private void StandbyMoving()
    {
        if (_enterDirection == EnterDirection.Left)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_rightBound, transform.position.y, transform.position.z), enemySpeed * Time.deltaTime);
        } else if (_enterDirection == EnterDirection.Top)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, _bottomBound, transform.position.z), enemySpeed * Time.deltaTime);

        } else if (_enterDirection == EnterDirection.Right)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_leftBound, transform.position.y, transform.position.z), enemySpeed * Time.deltaTime);
        }
    }

    private void EnableCustomMoving()
    {
        _customMovingEnabled = true;
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
    
    
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            GameManager.Instance.AddPoints(1);
        }

    }
}
