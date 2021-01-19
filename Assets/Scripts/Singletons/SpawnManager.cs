using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnManager : GenericSingletonClass<SpawnManager>
{
    public SpawnPoint[] spawnPoints;
    private Progress _progress;
    private BonusPoints _bonusPoints;


    public GameObject leftSpawn;
    public GameObject leftTopSpawn;
    public GameObject centerTopSpawn;
    public GameObject rightTopSpawn;
    public GameObject rightSpawn;
    void Start()
    {
        LoadComponent();
    }

    void Update()
    {
        float progress = GameManager.Instance.GetProgress() * 100;

        foreach (var spawnPoint in spawnPoints)
        {
            if (progress >= spawnPoint.progressRequired && !spawnPoint.spawned)
            {
                ExecuteSpawnPoint(spawnPoint);
            }
        }
    }

    private void LoadComponent()
    {
        _bonusPoints = GetComponent<BonusPoints>();
    }


    private void ExecuteSpawnPoint(SpawnPoint spawnPoint)
    {
        spawnPoint.spawned = true;

        int enemiesQuantity = CalculateEnemiesQuantity(spawnPoint);
        string enemyStackID = Get8CharacterRandomString();
        _bonusPoints.AddStack(enemyStackID, enemiesQuantity);

        
        if (spawnPoint.spawnPointIntensity.left > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, leftSpawn, spawnPoint.spawnPointIntensity.left, Enemy.EnterDirection.Left, enemyStackID);
        }
        if (spawnPoint.spawnPointIntensity.topLeft > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, leftTopSpawn, spawnPoint.spawnPointIntensity.topLeft, Enemy.EnterDirection.Top, enemyStackID);
        }
        if (spawnPoint.spawnPointIntensity.topCenter > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, centerTopSpawn, spawnPoint.spawnPointIntensity.topCenter, Enemy.EnterDirection.Top, enemyStackID);
        }
        if (spawnPoint.spawnPointIntensity.topRight > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, rightTopSpawn, spawnPoint.spawnPointIntensity.topRight, Enemy.EnterDirection.Top, enemyStackID);
        }
        if (spawnPoint.spawnPointIntensity.right > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, rightSpawn, spawnPoint.spawnPointIntensity.right, Enemy.EnterDirection.Right, enemyStackID);
        }
        


    }

    private int CalculateEnemiesQuantity(SpawnPoint spawnPoint)
    {
        return spawnPoint.spawnPointIntensity.left + spawnPoint.spawnPointIntensity.topLeft +
               spawnPoint.spawnPointIntensity.topCenter + spawnPoint.spawnPointIntensity.topRight + spawnPoint.spawnPointIntensity.right;
    }

    private void SpawnEnemy(GameObject enemyPrefab, GameObject spawnLocationGameObject, int enemiesQuantityOnSpawnLocation, Enemy.EnterDirection enemyEnterDirection, string enemyStackID)
    {
        for (int i = 0; i < enemiesQuantityOnSpawnLocation; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(spawnLocationGameObject.transform.position.x + (0.4f * i), spawnLocationGameObject.transform.position.y + (0.4f * i), spawnLocationGameObject.transform.position.z),
                spawnLocationGameObject.transform.rotation);
            enemy.GetComponent<Enemy>().SetEnemyStackID(enemyStackID);
            enemy.GetComponent<Enemy>()._enterDirection = enemyEnterDirection;
        }

    }
    
    private string Get8CharacterRandomString()
    {
        string path = Path.GetRandomFileName();
        path = path.Replace(".", ""); // Remove period.
        return path.Substring(0, 8);  // Return 8 character string
    }



}



[Serializable]
public class SpawnPoint
{
    public float progressRequired;
    public GameObject enemyPrefabToSpawn;
    public SpawnPointIntensity spawnPointIntensity;
    public bool spawned = false;
}

[Serializable]
public class SpawnPointIntensity
{
    public int left;
    public int topLeft;
    public int topCenter;
    public int topRight;
    public int right;
}