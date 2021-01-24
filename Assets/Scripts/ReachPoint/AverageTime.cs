using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AverageTime : TextDelayAnimation
{
    private TextMeshProUGUI _yourTime;

    private float _averageTime;
    void Start()
    {
        LoadComponents();
        _averageTime = GameManager.Instance.GetAverageTime(StageManager.Instance.GetCurrentStage());
        fullText = string.Format("{0, 3:000}", _averageTime);
    }

    private void LoadComponents()
    {
        _yourTime = GetComponent<TextMeshProUGUI>();
        reachPointTextController = GameObject.Find("ReachPoint").GetComponent<ReachPointTextController>();
    }

    void Update()
    {
        if (!showTextStarted && reachPointTextController.CanAnimateAverageTimeScore())
        {
            StartCoroutine(ShowText(_yourTime));
        }
        
        if (reachPointTextController.CanAnimateBonusPointExchange())
        {
            _yourTime.text = string.Format("{0, 3:000}", _averageTime);
        }
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        reachPointTextController.AnimateAverageTimeScore();
    }

    public float GetAverageTime()
    {
        return _averageTime;
    }

    public void RemoveOneSecond()
    {
        _averageTime--;
    }
}
