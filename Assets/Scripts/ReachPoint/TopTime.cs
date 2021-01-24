using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopTime : TextDelayAnimation
{    
    private TextMeshProUGUI _topTime;
    
    void Start()
    {
        LoadComponents();
        fullText = string.Format("{0, 3:000}", GameManager.Instance.GetTopRecord(StageManager.Instance.GetCurrentStage()));
    }

    private void LoadComponents()
    {
        _topTime = GetComponent<TextMeshProUGUI>();
        reachPointTextController = GameObject.Find("ReachPoint").GetComponent<ReachPointTextController>();
    }

    void Update()
    {
        if (!showTextStarted && reachPointTextController.CanAnimateYourTopRecordScore())
        {
            StartCoroutine(ShowText(_topTime));
        }
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        reachPointTextController.AnimateYourTopRecordScore();
    }
}
