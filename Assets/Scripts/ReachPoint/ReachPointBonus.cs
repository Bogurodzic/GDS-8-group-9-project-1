using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReachPointBonus : TextDelayAnimation
{
    
    protected TextMeshProUGUI _text;
    private string _currentText = "";
    public int baseBonusPoints = 1000;
    void Start()
    {
        LoadComponents();
        fullText = GetBonusPoint().ToString();
    }
    
    private void LoadComponents()
    {
        _text = GetComponent<TextMeshProUGUI>();
        reachPointTextController = GameObject.Find("ReachPoint").GetComponent<ReachPointTextController>();
    }

    void Update()
    {
        if (!showTextStarted && reachPointTextController.CanAnimateSpecialBonusScore())
        {
            StartCoroutine(ShowText(_text));
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
    

}
