using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoader : MonoBehaviour
{
    private LinkedList<ObstacleMapped> _obstacles = new LinkedList<ObstacleMapped>();
    private float _stageWidth;
    
    public GameObject crystalSmallPrefab;
    public GameObject crystalBigPrefab;
    public GameObject hole1Prefab;
    public GameObject hole2Prefab;
    public GameObject smallHole1Prefab;
    public GameObject smallHole2Prefab;
    public GameObject minePrefab;
    public GameObject rollingRockSmallPrefab;
    public GameObject rollingRockBigPrefab;

    public GameObject checkPointPrefab;
    public GameObject stagePointPrefab;
    
    void Start()
    {
        _stageWidth = GameManager.Instance.GetCurrentStageWidth();
        MapGameObjectWithPoints("Obstacle");
        MapGameObjectWithPoints("Hole");
        MapGameObjectWithPoints("CheckPoint");
        MapGameObjectWithPoints("StagePoint");
        DestroyAllObjectsWithTag("Obstacle");
        DestroyAllObjectsWithTag("Hole");
        DestroyAllObjectsWithTag("CheckPoint");
        DestroyAllObjectsWithTag("StagePoint");
        ResetObstaclesOnMap();
    }

    void Update()
    {
        
    }

    private GameObject[] LoadAllGameObjectsWithTag(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag);
    }

    private void MapGameObjectWithPoints(string tag)
    {
        GameObject[] obstacles = LoadAllGameObjectsWithTag(tag);

        foreach (var obstacle in obstacles)
        {
            ObstacleMapped obstacleMapped = new ObstacleMapped(obstacle, obstacle.transform.position);
            _obstacles.AddLast(obstacleMapped);
        }
    }
    
    public void DestroyAllObjectsWithTag(string tag)
    {
        GameObject[] obstacles = LoadAllGameObjectsWithTag(tag);

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
            float newObstacleXPosition = obstacleMapped.GetObstacleInitialPosition().x - (_stageWidth * GameManager.Instance.GetLastProgressCheckpoint());
            GameObject obstacle = Instantiate(obstaclePrefab, new Vector3(newObstacleXPosition, obstacleMapped.GetObstacleInitialPosition().y, obstacleMapped.GetObstacleInitialPosition().z), Quaternion.identity);
        } 
    }


    private GameObject GetObstaclePrefab(ObstacleMapped obstacleMapped)
    {
        
        Debug.Log("OBSTACLE NAME: " + obstacleMapped.GetGameObjectName());
        if (obstacleMapped.GetGameObjectName() == "CrystalSmall")
        {
            return crystalSmallPrefab;
        } else if (obstacleMapped.GetGameObjectName() == "CrystalBig")
        {
            return crystalBigPrefab;
        } else if (obstacleMapped.GetGameObjectName() == "Hole1")
        {
            
            Debug.Log("HOLE1 PREFAB!!!!!!");
            return hole1Prefab;
        } else if (obstacleMapped.GetGameObjectName() == "Hole2")
        {
            Debug.Log("HOLE2 PREFAB!!!!!!");

            return hole2Prefab;
        } else if (obstacleMapped.GetGameObjectName() == "SmallHole1")
        {
            return smallHole1Prefab;
        } else if (obstacleMapped.GetGameObjectName() == "SmallHole2")
        {
            return smallHole2Prefab;
        }else if (obstacleMapped.GetGameObjectName() == "Mine")
        {
            return minePrefab;
        } else if (obstacleMapped.GetGameObjectName() == "RollingRockSmall")
        {
            return rollingRockSmallPrefab;
        } else if (obstacleMapped.GetGameObjectName() == "RollingRockBig")
        {
            return rollingRockBigPrefab;
        } else if (obstacleMapped.GetGameObjectName() == "CheckPoint")
        {
            return checkPointPrefab;
        } else if (obstacleMapped.GetGameObjectName() == "StagePoint")
        {
            return stagePointPrefab;
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
    private string _gameObjectName;
    private Vector3 _initialPosition;

    public ObstacleMapped(GameObject obstacle, Vector3 initialPosition)
    {
        _obstacle = obstacle;
        _initialPosition = initialPosition;
        _gameObjectName = obstacle.name;
    }

    public GameObject GetObstacleGameObject()
    {
        return _obstacle;
    }

    public Vector3 GetObstacleInitialPosition()
    {
        return _initialPosition;
    }

    public string GetGameObjectName()
    {
        return _gameObjectName;
    }
}