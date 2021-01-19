using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : GenericSingletonClass<SpawnManager>
{
    public SpawnPoint[] spawnPoints;
    private Progress _progress;


    public GameObject leftSpawn;
    public GameObject leftTopSpawn;
    public GameObject centerTopSpawn;
    public GameObject rightTopSpawn;
    public GameObject rightSpawn;
    void Start()
    {
        
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


    private void ExecuteSpawnPoint(SpawnPoint spawnPoint)
    {
        spawnPoint.spawned = true;

        int enemiesQuantity = spawnPoint.spawnPointIntensity.left + spawnPoint.spawnPointIntensity.topLeft +
                              spawnPoint.spawnPointIntensity.topCenter + spawnPoint.spawnPointIntensity.topRight + spawnPoint.spawnPointIntensity.right;
        
        if (spawnPoint.spawnPointIntensity.left > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, leftSpawn, enemiesQuantity);
        }
        if (spawnPoint.spawnPointIntensity.topLeft > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, leftTopSpawn, enemiesQuantity);
        }
        if (spawnPoint.spawnPointIntensity.topCenter > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, centerTopSpawn, enemiesQuantity);
        }
        if (spawnPoint.spawnPointIntensity.topRight > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, rightTopSpawn, enemiesQuantity);
        }
        if (spawnPoint.spawnPointIntensity.right > 0)
        {
            SpawnEnemy(spawnPoint.enemyPrefabToSpawn, rightSpawn, enemiesQuantity);
        }
        
        Debug.Log("SPAWNED ENEMY: " + spawnPoint.progressRequired + " ::: quantity: " + enemiesQuantity );

    }

    private void SpawnEnemy(GameObject enemyPrefab, GameObject spawnLocationGameObject, int enemiesQuantity)
    {
        Instantiate(enemyPrefab, spawnLocationGameObject.transform.position,
            spawnLocationGameObject.transform.rotation);
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