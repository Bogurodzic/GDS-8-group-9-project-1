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
    
    private BoxCollider2D _boxCollider;
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
            TurnOnBoxCollider();
        }
        else
        {
            TurnOffBoxCollider();
        }
    }

    private void TurnOnBoxCollider()
    {
        if (_boxCollider)
        {
            _boxCollider.enabled = true;
        }  
    }


    private void TurnOffBoxCollider()
    {
        if (_boxCollider)
        {
            _boxCollider.enabled = false;
        }
    }

    private void UpdateGameSpeed()
    {
        if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Normal)
        {
            _landSpeed = _initLandSpeed * speedRatio;
        }
        else if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Fast)
        {
            _landSpeed = _initLandSpeed * 1.3f * speedRatio;
        } else if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Slow)
        {
            _landSpeed = _initLandSpeed * 0.7f * speedRatio;
        }
    }

    private void LoadComponents()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void MoveLand()
    {
        transform.Translate(Vector3.left * _landSpeed * Time.deltaTime);
    }
    
}
