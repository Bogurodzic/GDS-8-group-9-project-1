﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingletonClass<GameManager>
{
    public enum PlayerSpeed
    {
        Slow,
        Normal,
        Fast
    }

    private float _timer;
    private float[] _topRecord = new float[5];
    public float[] averageTime = new float[5];

    public float gameSpeed;

    private PlayerSpeed _playerSpeed = PlayerSpeed.Normal;
    private Boolean _gameRunning = false;
    private Boolean _gameStarted = false;

    private bool _playerFallenInHole = false;
    
    public int pointsRequiredForBonusLive = 10000;
    private int _bonusLivesAcquired;
    private int _currentScore = 0;
    private int _highScore = 0;

    public int _maxBombAmonut = 3;
    private int _currentBombAmount = 0;
    public int _maxCraterBombAmount = 1;
    private int _currentCraterBombAmount = 0;

    private int _initLivesAmount = 4;
    private int _currentLivesAmount = 4;

    private float _progress = 0;
    private float _lastProgressCheckpoint = 0;

    public void Start()
    {
        LoadSaveData();
    }

    public void StartBackgroundMusic()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play();
    }

    public void SetTimer(float timer)
    {
        _timer = timer;
    }

    public float GetTimer()
    {
        return _timer;
    }

    public void ResetTimer()
    {
        _timer = 0;
    }

    public float GetAverageTime(int stage)
    {
        return averageTime[stage - 1];
    }

    public float[] GetAllRecords()
    {
        return _topRecord;
    }

    public float GetTopRecord(int stage)
    {
        if (_topRecord[stage - 1] > 0)
        {
            return _topRecord[stage - 1];
        }
        else
        {
            return averageTime[stage - 1];
        }
    }

    public void SetTopRecord(int stage, int recordScore)
    {
        _topRecord[stage - 1] = recordScore;
    }

    public void PauseGame()
    {
        this._gameRunning = false;
    }

    public void PlayGame()
    {
        _gameRunning = true;
    }
    
    public Boolean IsGameRunning()
    {
        return _gameRunning;
    }

    public void StopGame()
    {
        _gameStarted = false;
    }
    
    public void StartGame()
    {
        _gameStarted = true;
    }

    public bool IsGameStarted()
    {
        return _gameStarted;
    }
    
    


    public PlayerSpeed GetPlayerSpeed()
    {
        return this._playerSpeed;
    }

    public void SetSlowPlayerSpeed()
    {
        this._playerSpeed = PlayerSpeed.Slow;
    }
    
    public void SetNormalPlayerSpeed()
    {
        this._playerSpeed = PlayerSpeed.Normal;
    }
    
    public void SetFastPlayerSpeed()
    {
        this._playerSpeed = PlayerSpeed.Fast;
    }

    public void AddPoints(int points)
    {
        _currentScore += points;
        HandleBonusLifeForPoints();
        RefreshHighScore();
    }

    private void HandleBonusLifeForPoints()
    {
        double liveExchangedForPoints = Math.Floor((double) (_currentScore / pointsRequiredForBonusLive));
        
        int livesToAdd = (int) (liveExchangedForPoints - _bonusLivesAcquired);
        _currentLivesAmount += livesToAdd;
        _bonusLivesAcquired += livesToAdd;
    }

    private void RefreshHighScore()
    {
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
        } 
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public int GetHighScore()
    {
        return _highScore;
    }

    public void ResetScore()
    {
        _currentScore = 0;
    }

    public void BombDeployed()
    {
        _currentBombAmount += 1;
    }

    public bool CanBombBeSpawned()
    {
        return _currentBombAmount < _maxBombAmonut;
    }

    public void BombDestroyed()
    {
        if (_currentBombAmount > 0)
        {
            _currentBombAmount -= 1;
        }
    }
    
    public void CraterBombDemployed()
    {
        _currentCraterBombAmount += 1;
    }

    public bool CanCraterBombBeSpawned()
    {
        return _currentCraterBombAmount < _maxCraterBombAmount;
    }

    public void CraterBombDestroyed()
    {
        if (_currentCraterBombAmount > 0)
        {
            Invoke("DecreaseCraterBombAmountByOne", 1f);
        }
    }

    private void DecreaseCraterBombAmountByOne()
    {
        _currentCraterBombAmount -= 1;
    }

    public void ResetBombDeployed()
    {
        _currentBombAmount = 0;
        _currentCraterBombAmount = 0;
    }

    public int GetPlayerLives()
    {
        return _currentLivesAmount;
    }

    public bool CanRespawnPlayer()
    {
        return _currentLivesAmount > 0;
    }

    public void DecreaseLivesAmount()
    {
        if (_currentLivesAmount > 0)
        {
            _currentLivesAmount -= 1;
        }
    }

    public void ResetLivesAmount()
    {
        _currentLivesAmount = _initLivesAmount;
    }

    public void RespawnPlayer()
    {
        ResetBombDeployed();
        PlayGame();
        DecreaseLivesAmount();
        ResetLevel();
    }

    private void ResetLevel()
    {
        ResetLandWrapper();
        Reset1stBackgroundLayer();
        Reset2ndBackgroundLayer();
        ResetEnemies();
        ResetProjectiles();
        ResetCraters();
        ResetObstacles();
        ResetHoles();
        ResetCheckPoints();
        SetPlayerFallenInHole(false);
        ReserObstaclesOnMap();
        ResetBombDeployed();
    }
    
    public void ResetGame()
    {
        
        ResetLevel();
        ResetScore();
        ResetLivesAmount();
        ResetTimer();
        SaveData();
        StopGame();
        GoToMenu();
    }


    private void ReserObstaclesOnMap()
    {
        GameObject.Find("ObstacleLoader").GetComponent<ObstacleLoader>().ResetObstaclesOnMap();
    }

    private void ResetLandWrapper()
    {
        ProgressController landWrapperProgressController = GameObject.Find("LandWrapper").GetComponent<ProgressController>();
        landWrapperProgressController.SetGameObjectProgress(GetLastProgressCheckpoint());
    }

    private void Reset1stBackgroundLayer()
    {
        
        ProgressController background1stLayerProgressController = GameObject.Find("Background1stLayer").GetComponent<ProgressController>();
        background1stLayerProgressController.SetGameObjectProgress(GetLastProgressCheckpoint());
    }
    
    private void Reset2ndBackgroundLayer()
    {
        
        ProgressController background2ndLayerProgressController = GameObject.Find("Background2ndLayer").GetComponent<ProgressController>();
        background2ndLayerProgressController.SetGameObjectProgress(GetLastProgressCheckpoint());
    }

    private void ResetEnemies()
    {
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().ResetSpawns();
        DestroyAll("Enemy");
        DestroyAll("EnemyTank");
    }
    
    private void ResetProjectiles()
    {
        DestroyAll("Projectile");
        DestroyAll("EnemyProjectile");
        DestroyAll("EnemyProjectileExplosive");
    }

    private void ResetCraters()
    {
        DestroyAll("CraterPlatform");
    }

    private void ResetObstacles()
    {
        DestroyAll("Obstacle");
    }

    private void ResetHoles()
    {
        DestroyAll("Hole");
    }

    private void ResetCheckPoints()
    {
        DestroyAll("CheckPoint");
        DestroyAll("StagePoint");
        DestroyAll("CautionPoint");

    }

    private void DestroyAll(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (tag);
     
        for(var i = 0 ; i < gameObjects.Length ; i ++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public void PlayerFallingInHole()
    {
        SetPlayerFallenInHole(true);
        PlayerDeath();
    }

    public void PlayerDeath()
    {
        PauseGame();
    }


    public void SetProgress(float progress)
    {
        _progress = progress;
    }

    public float GetProgress()
    {
        return _progress;
    }

    public void SetLastProgressCheckpoint(float progress)
    {
        _lastProgressCheckpoint = progress;
    }

    public float GetLastProgressCheckpoint()
    {
        return _lastProgressCheckpoint;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Scenes/menu");
    }


    public float GetGameSpeed()
    {
        return gameSpeed;
    }

    public float GetCurrentStageWidth()
    {
        return GameObject.Find("LandWrapper").GetComponent<BoxCollider2D>().size.x;
    }


    public void SaveData()
    {
        Save save = PrepareSave();
        string jsonData = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/MoonPatrol_Save", jsonData);
        
        
    }

    private Save PrepareSave()
    {
        Save save = new Save(GameManager.Instance.GetAllRecords(), GameManager.Instance.GetHighScore());
        return save;
    }

    public void LoadSaveData()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/MoonPatrol_Save"))
        {
            string jsonData = File.ReadAllText(Application.persistentDataPath + "/MoonPatrol_Save");
            Save save = JsonUtility.FromJson<Save>(jsonData);

            _highScore = save.TopScore;
            _topRecord = save.TopTime;
        }
    }

    private void SetPlayerFallenInHole(bool playerFallenInHole)
    {
        _playerFallenInHole = playerFallenInHole;
    }

    public bool GetPlayerFallenInHole()
    {
        return _playerFallenInHole;
    }
}

public class Save
{
    public float[] TopTime;
    public int TopScore;
    public Save(float[] topTime, int topScore)
    {
        TopTime = topTime;
        TopScore = topScore;
    }
}