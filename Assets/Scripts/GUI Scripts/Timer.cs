using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    void Update () {
        if(GameManager.Instance.IsGameStarted())
        {
            float timePassed = CalculatePassedTime();
            SetGameTimer(timePassed);
            UpdateTimerText();
        }
    }
    private float CalculatePassedTime()
    {
        return GameManager.Instance.GetTimer() + Time.deltaTime;
    }

    private void SetGameTimer(float time)
    {
        GameManager.Instance.SetTimer(time);
    }

    private void UpdateTimerText()
    {
        timer.text = string.Format("{0, 3:000}", GameManager.Instance.GetTimer());
    }
    
}
