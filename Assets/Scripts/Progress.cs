﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    private BoxCollider2D _landWrapperBoxCollider2D;
    private GameObject _playerCar;
    private float _playerInitPosition;
    private float _landInitMinPosition;
    private float _landInitMaxPosition;
    void Start()
    {
        LoadComponents();
        SavePlayerInitPosition();
        _landInitMinPosition = _landWrapperBoxCollider2D.bounds.min.x;
        _landInitMaxPosition = _landWrapperBoxCollider2D.bounds.max.x;
    }

    void Update()
    {
        UpdateProgress();
    }

    private void LoadComponents()
    {
        _landWrapperBoxCollider2D = GetComponent<BoxCollider2D>();
        _playerCar = GameObject.Find("Car");
    }

    private void SavePlayerInitPosition()
    {
        _playerInitPosition = _playerCar.transform.position.x;
    }

    private float CalculateProgress()
    {

        float currentPosition = _landWrapperBoxCollider2D.bounds.min.x - _playerInitPosition;
        float width = _landInitMaxPosition - _landInitMinPosition;
        float progress = currentPosition / width * 100;
        progress = (float) Math.Floor(Math.Abs(progress));
        return progress;
    }

    private void UpdateProgress()
    {
        GameManager.Instance.SetProgress(CalculateProgress());
    }
}
