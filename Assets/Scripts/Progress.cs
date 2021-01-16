using System;
using System.Collections;
using System.Collections.Generic;
using GUI_Scripts;
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

        float currentPosition = CalculateCurrentPosition();
        float width = GetGameWidth();
        float progress = currentPosition / width * -1;
        if (progress < 0)
        {
            progress = progress * -1;
        }
        return progress;
    }

    private void UpdateProgress()
    {
        float progress = CalculateProgress();
        GameManager.Instance.SetProgress(progress);
    }

    public float CalculateCurrentPosition()
    {
        return _landWrapperBoxCollider2D.bounds.min.x - _playerInitPosition;
    }
    
    public float GetCurrentPosition()
    {
        return CalculateCurrentPosition();
    }

    public float GetGameWidth()
    {   
        Debug.Log("land min: " + _landInitMinPosition + " Land max: " + _landInitMaxPosition);
        return _landInitMaxPosition - _landInitMinPosition;
    }

    public float CalcHowFarObjectIsFromInitialPosition(float objectPosition)
    {
        float distance;
        Debug.Log("CalcHowFarObjectIsFromInitialPosition: " + _landInitMinPosition + " ::: " + objectPosition);
        if (_landInitMinPosition < 0 && objectPosition < 0)
        {
            Debug.Log("CalcHowFarObjectIsFromInitialPosition1: " + _landInitMinPosition + " ::: " + objectPosition + " ::: " + (_landInitMinPosition - objectPosition));

            distance = _landInitMinPosition - objectPosition;
        } else if (_landInitMinPosition < 0 && objectPosition >= 0)
        {
            Debug.Log("CalcHowFarObjectIsFromInitialPosition2: " + _landInitMinPosition + " ::: " + objectPosition + " ::: " + (-_landInitMinPosition + objectPosition));

            distance =  -_landInitMinPosition + objectPosition;
        } else if (_landInitMinPosition >= 0 && objectPosition >= 0)
        {
            Debug.Log("CalcHowFarObjectIsFromInitialPosition3: " + _landInitMinPosition + " ::: " + objectPosition + " ::: " + (_landInitMinPosition + objectPosition));

            distance = _landInitMinPosition + objectPosition;
        }
        else
        {
            Debug.Log("CalcHowFarObjectIsFromInitialPosition4: " + _landInitMinPosition + " ::: " + objectPosition + " ::: " + (_landInitMinPosition + objectPosition));

            distance = _landInitMinPosition + objectPosition;
        }

        if (distance >= 0)
        {
            return distance;
        }
        else
        {
            return distance * -1;
        }
    }
}
