using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextPoint : TextDelayAnimation
{
    public TextMeshProUGUI nextReachPoint;
    void Start()
    {
        LoadComponents();
        fullText = "Time to reach point '" + (StageManager.Instance.GetCurrentStage() + 1) + "'";
    }

    private void LoadComponents()
    {
        nextReachPoint = GetComponent<TextMeshProUGUI>();
        reachPointTextController = GameObject.Find("ReachPoint").GetComponent<ReachPointTextController>();
    }

    void Update()
    {
        if (!showTextStarted && reachPointTextController.CanAnimateMainText())
        {
            StartCoroutine(ShowText(nextReachPoint));
        }
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        Debug.Log("EXECUTE SHITTT");
        reachPointTextController.AnimateMainText();
    }
}
