using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReachPointBonus : TextDelayAnimation
{
    
    protected TextMeshProUGUI _text;
    private string _currentText = "";
    public int baseBonusPoints = 1000;

    public float exchangePointsDelay = 0.2f;
    private int _currentPoints;
    private bool _exchangePointsStarted = false;
    private AverageTime _averageTime;
    void Start()
    {
        LoadComponents();
        fullText = baseBonusPoints + "";
        _currentPoints = baseBonusPoints;
        GameManager.Instance.AddPoints(_currentPoints);
    }
    
    private void LoadComponents()
    {
        _text = GetComponent<TextMeshProUGUI>();
        reachPointTextController = GameObject.Find("ReachPoint").GetComponent<ReachPointTextController>();
        _averageTime = GameObject.Find("ATime").GetComponent<AverageTime>();
    }

    void Update()
    {
        if (!showTextStarted && reachPointTextController.CanAnimateSpecialBonusScore())
        {
            StartCoroutine(ShowText(_text));
        }

        if (!_exchangePointsStarted && reachPointTextController.CanAnimateBonusPointExchange())
        {
            StartCoroutine(ExchangePoints(_text));
        }
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        reachPointTextController.AnimateSpecialBonusScore();
    }

    private int GetBonusPoint()
    {
        if (GameManager.Instance.GetTimer() <
            GameManager.Instance.GetAverageTime(StageManager.Instance.GetCurrentStage()))
        {
            int timeDifference =   (int) (GameManager.Instance.GetAverageTime(StageManager.Instance.GetCurrentStage()) -
                                          GameManager.Instance.GetTimer());
            return baseBonusPoints + (timeDifference * 100);
        }
        else
        {
            return baseBonusPoints;
        }
    }
    
    private IEnumerator ExchangePoints(TextMeshProUGUI text)
    {
        _exchangePointsStarted = true;
        
       while (Math.Round(GameManager.Instance.GetTimer()) <
              _averageTime.GetAverageTime())
       {
           _currentPoints += 100;
           GameManager.Instance.AddPoints(100);
           _averageTime.RemoveOneSecond();
           text.text = _currentPoints + "";
           yield return new WaitForSeconds(exchangePointsDelay);
       }
    }
    

}
