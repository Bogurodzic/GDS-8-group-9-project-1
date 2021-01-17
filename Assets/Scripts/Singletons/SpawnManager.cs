using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : GenericSingletonClass<SpawnManager>
{
    public SpawnPoint[] SpawnPoint;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}


[Serializable]
public class SpawnPoint
{
    public float progressRequired;
    public GameObject enemyPrefabToSpawn;
    public SpawnPointIntensity spawnPointIntensity;
}

[Serializable]
public class SpawnPointIntensity
{
    public double left;
    public double topLeft;
    public double topCenter;
    public double topRight;
    public double right;
}