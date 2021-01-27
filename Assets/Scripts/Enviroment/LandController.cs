using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController : MonoBehaviour
{
    public float speedRatio = 1;
    private float _landSpeed;
    private float _initLandSpeed;
    private float _startingHorizontalPosition;

    private Rigidbody2D _rigidbody;
    
    void Start()
    {
        LoadComponents();
        
        _startingHorizontalPosition = transform.position.x;
        _landSpeed = GameManager.Instance.GetGameSpeed();
        _initLandSpeed = _landSpeed;
    }
    void Update()
    {
        if (GameManager.Instance.IsGameRunning())
        {
            UpdateGameSpeed();
            MoveLand();
        }

        if (!GameManager.Instance.IsGameRunning() && _rigidbody)
        {
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (GameManager.Instance.IsGameRunning() && _rigidbody)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

    }

    private void LoadComponents()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void UpdateGameSpeed()
    {
        if (IsGameSpeedNormal())
        {
            NormalizeLandSpeed();
        }
        else if (IsGameSpeedFast())
        {
            SpeedUpLandSpeed();
        } else if (IsGameSpeedSlow())
        {
            SlowDownLandSpeed();
        }
    }

    private bool IsGameSpeedNormal()
    {
        return GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Normal;
    }

    private bool IsGameSpeedFast()
    {
        return GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Fast;
    }

    private bool IsGameSpeedSlow()
    {
        return GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Slow;
    }

    private void NormalizeLandSpeed()
    {
        _landSpeed = _initLandSpeed * speedRatio;
    }

    private void SpeedUpLandSpeed()
    {
        _landSpeed = _initLandSpeed * 1.3f * speedRatio;
    }

    private void SlowDownLandSpeed()
    {
        _landSpeed = _initLandSpeed * 0.7f * speedRatio;
    }

    private void MoveLand()
    {
        transform.Translate(Vector3.left * _landSpeed * Time.deltaTime);
    }
    
}
