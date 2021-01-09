using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    public enum PlayerSpeed
    {
        Slow,
        Normal,
        Fast
    }

    private PlayerSpeed _playerSpeed = PlayerSpeed.Normal;
    private Boolean _gameRunning = true;

    private int _currentScore = 0;
    private int _highScore = 0;

    private int _maxBombAmonut = 2;
    private int _currentBombAmount = 0;

    public void StopGame()
    {
        this._gameRunning = false;
    }
    public Boolean IsGameRunning()
    {
        return this._gameRunning;
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
        RefreshHighScore();
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
}