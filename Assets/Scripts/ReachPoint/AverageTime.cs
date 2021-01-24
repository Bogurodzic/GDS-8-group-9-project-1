using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AverageTime : TextDelayAnimation
{
    private TextMeshProUGUI _yourTime;
    void Start()
    {
        LoadComponents();
        fullText = string.Format("{0, 3:000}", GameManager.Instance.GetAverageTime(StageManager.Instance.GetCurrentStage()));
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
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        reachPointTextController.AnimateAverageTimeScore();
    }
}
