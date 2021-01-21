using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoader : MonoBehaviour
{
    private LinkedList<ObstacleMapped> _obstacles = new LinkedList<ObstacleMapped>();
    
    public GameObject crystalSmallPrefab;
    public GameObject crystalBigPrefab;
    public GameObject hole1Prefab;
    public GameObject hole2Prefab;
    public GameObject smallHole1Prefab;
    public GameObject smallHole2Prefab;
    public GameObject minePrefab;
    public GameObject rollingRockSmallPrefab;
    public GameObject rollingRockBigPrefab;
    
    
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
            return crystalSmallPrefab;
        } else if (obstacleMapped.GetObstacleGameObject().name == "CrystalBig")
        {
            return crystalBigPrefab;
        } else if (obstacleMapped.GetObstacleGameObject().name == "Hole1")
        {
            return hole1Prefab;
        } else if (obstacleMapped.GetObstacleGameObject().name == "Hole2")
        {
            return hole2Prefab;
        } else if (obstacleMapped.GetObstacleGameObject().name == "SmallHole1")
        {
            return smallHole1Prefab;
        } else if (obstacleMapped.GetObstacleGameObject().name == "SmallHole2")
        {
            return smallHole2Prefab;
        }else if (obstacleMapped.GetObstacleGameObject().name == "Mine")
        {
            return minePrefab;
        } else if (obstacleMapped.GetObstacleGameObject().name == "RollingRockSmall")
        {
            return rollingRockSmallPrefab;
        } else if (obstacleMapped.GetObstacleGameObject().name == "RollingRockBig")
        {
            return rollingRockBigPrefab;
        }
        else
        {
            return crystalSmallPrefab;
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