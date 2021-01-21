﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoader : MonoBehaviour
{
    private LinkedList<ObstacleMapped> _obstacles = new LinkedList<ObstacleMapped>();
    
    public GameObject crystalPrefab;
    void Start()
    {
        MapObstaclesWithPoints();
        DestroyAllObstacles();
        ResetObstaclesOnMap();
    }

    void Update()
    {
        
    }

    private GameObject[] LoadAllObstacles()
    {
        return GameObject.FindGameObjectsWithTag("Obstacle");
    }

    private void MapObstaclesWithPoints()
    {
        GameObject[] obstacles = LoadAllObstacles();

        foreach (var obstacle in obstacles)
        {
            Debug.Log("OBSTACLE POSITION: " +  obstacle.transform.position + " OBSTACLE NAME: " + obstacle.name);
            ObstacleMapped obstacleMapped = new ObstacleMapped(obstacle, obstacle.transform.position);
            _obstacles.AddLast(obstacleMapped);
        }
    }

    public void DestroyAllObstacles()
    {
        GameObject[] obstacles = LoadAllObstacles();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    public void ResetObstaclesOnMap()
    {
        LinkedList<ObstacleMapped>.Enumerator obstaclesEnumerator = _obstacles.GetEnumerator();
        while (obstaclesEnumerator.MoveNext())
        {
            ObstacleMapped obstacleMapped = obstaclesEnumerator.Current;
            GameObject obstaclePrefab = GetObstaclePrefab(obstacleMapped);
            GameObject obstacle = Instantiate(obstaclePrefab, obstacleMapped.GetObstacleInitialPosition(),
                obstacleMapped.GetObstacleGameObject().transform.rotation);
        } 
    }


    private GameObject GetObstaclePrefab(ObstacleMapped obstacleMapped)
    {
        if (obstacleMapped.GetObstacleGameObject().name == "CrystalSmall")
        {
            return crystalPrefab;
        }
        else
        {
            return crystalPrefab;
        }
    }
}

class ObstacleMapped
{

    private GameObject _obstacle;
    private Vector3 _initialPosition;
    
    public ObstacleMapped(GameObject obstacle, Vector3 initialPosition)
    {
        _obstacle = obstacle;
        _initialPosition = initialPosition;
    }

    public GameObject GetObstacleGameObject()
    {
        return _obstacle;
    }

    public Vector3 GetObstacleInitialPosition()
    {
        return _initialPosition;
    }
}