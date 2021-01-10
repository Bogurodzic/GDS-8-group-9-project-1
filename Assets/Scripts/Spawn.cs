using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;

public class Spawn : MonoBehaviour
{
    public GameObject[] enemiesPrefabs;
    
    private GameObject _enemyArea;
    private BoxCollider2D _enemyAreaBoxCollider;
    
    private float _leftBound;
    private float _rightBound;
    private float _topBound;
    private float _bottomBound;
    private Vector3 _enemiesSpawnPosition;

    public float firstSpawnTime;
    public float minWaitBeforeSpawn;
    public float maxWaitBeforeSpwan;
    private bool _isEnemySpawning = false;

    public SpawnLocation enemySpawnLocation;

    public enum SpawnLocation
    {
        Left,
        Top,
        Right
    }
    void Start()
    {
        _enemyArea = GameObject.FindGameObjectsWithTag("EnemyArea")[0];
        _enemyAreaBoxCollider = _enemyArea.GetComponent<BoxCollider2D>();
        CalculateEnemyAreaBounds();
        HandleSpawningEnemy(firstSpawnTime, firstSpawnTime);
    }

    void Update()
    {

        HandleSpawningEnemy(minWaitBeforeSpawn, maxWaitBeforeSpwan);
        
    }
    
    private void HandleSpawningEnemy(float minTime, float maxTime)
    {
        if (!_isEnemySpawning)
        {
            float timer = UnityEngine.Random.Range(minTime, maxTime);
            Invoke("SpawnEnemyOnItsPosition", timer);
            _isEnemySpawning = true;
        }

        if (!GameManager.Instance.IsGameRunning())
        {
            CancelInvoke("SpawnEnemyOnItsPosition");
        }
    }
 
    void SpawnEnemyOnItsPosition()
    {
        if (enemySpawnLocation == SpawnLocation.Left)
        {
            SpawnEnemyOnLeft();
        } else if (enemySpawnLocation == SpawnLocation.Top)
        {
            SpawnEnemyOnTop();
        } else if (enemySpawnLocation == SpawnLocation.Right)
        {
            SpawnEnemyOnRight();
        }
    }

    private void SpawnEnemyOnLeft()
    {
        int enemyQuantity = GenerateEnemyQuantity();
        _enemiesSpawnPosition = GenerateNewPosition();
        GameObject enemyToSpawn = GetRandomEnemyToSpawn();
        string enemyStackID = Get8CharacterRandomString();

        for (int i = 0; i < enemyQuantity; i++)
        {
            GameObject enemy = Instantiate(enemyToSpawn, new Vector3((_leftBound - 2) - (i * 1), _enemiesSpawnPosition.y, _enemiesSpawnPosition.z), enemyToSpawn.transform.rotation);
            enemy.GetComponent<Enemy>()._enterDirection = Enemy.EnterDirection.Left;
            enemy.GetComponent<Enemy>().SetEnemyStackID(enemyStackID);
        }       
        
        _isEnemySpawning = false;  
    }

    private void SpawnEnemyOnTop()
    {
        int enemyQuantity = GenerateEnemyQuantity();
        _enemiesSpawnPosition = GenerateNewPosition();
        GameObject enemyToSpawn = GetRandomEnemyToSpawn();
        string enemyStackID = Get8CharacterRandomString();
        GameObject.Find("SpawnManager").GetComponent<BonusPoints>().AddStack(enemyStackID, enemyQuantity);
        
        for (int i = 0; i < enemyQuantity; i++)
        {
            GameObject enemy = Instantiate(enemyToSpawn, new Vector3(( _enemiesSpawnPosition.x - 2) - (i * 1), _topBound + 2, _enemiesSpawnPosition.z), enemyToSpawn.transform.rotation);
            enemy.GetComponent<Enemy>()._enterDirection = Enemy.EnterDirection.Top;
            enemy.GetComponent<Enemy>().SetEnemyStackID(enemyStackID);

        }       
        
        _isEnemySpawning = false; 
    }

    private void SpawnEnemyOnRight()
    {
        int enemyQuantity = GenerateEnemyQuantity();
        _enemiesSpawnPosition = GenerateNewPosition();
        GameObject enemyToSpawn = GetRandomEnemyToSpawn();
        string enemyStackID = Get8CharacterRandomString();

        for (int i = 0; i < enemyQuantity; i++)
        {
            GameObject enemy = Instantiate(enemyToSpawn, new Vector3(( _rightBound + 2) - (i * 1),  _enemiesSpawnPosition.y, _enemiesSpawnPosition.z), enemyToSpawn.transform.rotation);
            enemy.GetComponent<Enemy>()._enterDirection = Enemy.EnterDirection.Right;
            enemy.GetComponent<Enemy>().SetEnemyStackID(enemyStackID);

        }       
        
        _isEnemySpawning = false;  
    }

    private GameObject GetRandomEnemyToSpawn()
    {
        return enemiesPrefabs[new System.Random().Next(enemiesPrefabs.Length)];
    }
    
    private string Get8CharacterRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path.Substring(0, 8);  // Return 8 character string
    }

    private int GenerateEnemyQuantity()
    {
        return Random.Range(3, 5);
    }
    
    private void CalculateEnemyAreaBounds()
    {
        _leftBound = _enemyAreaBoxCollider.bounds.min.x;
        _rightBound =  _enemyAreaBoxCollider.bounds.max.x;
        _topBound = _enemyAreaBoxCollider.bounds.max.y;
        _bottomBound = _enemyAreaBoxCollider.bounds.center.y;

    }
    
    private float GenerateNewXPosition()
    {
        return Random.Range(_leftBound, _rightBound);
    }

    private float GenerateNewYPosition()
    {
        return Random.Range(_topBound, _bottomBound);
    }

    private Vector3 GenerateNewPosition()
    {
        return new Vector3(GenerateNewXPosition(), GenerateNewYPosition(), 0);
    }
}
