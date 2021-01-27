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
        CheckRecord();
        fullText = string.Format("{0, 3:000}", GameManager.Instance.GetTopRecord(StageManager.Instance.GetCurrentStage()));
    }

    private void CheckRecord()
    {
        if ((int) GameManager.Instance.GetTimer() <
            (int) GameManager.Instance.GetTopRecord(StageManager.Instance.GetCurrentStage()))
        {
            
            GameManager.Instance.SetTopRecord(StageManager.Instance.GetCurrentStage(), (int) GameManager.Instance.GetTimer());
            reachPointTextController.SetNewRecord();
        } 
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
