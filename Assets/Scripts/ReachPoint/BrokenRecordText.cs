using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrokenRecordText : TextDelayAnimation
{
    public TextMeshProUGUI text;
    void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        text = GetComponent<TextMeshProUGUI>();
        reachPointTextController = GameObject.Find("ReachPoint").GetComponent<ReachPointTextController>();
    }

    void Update()
    {
        if (!showTextStarted && reachPointTextController.CanAnimateRecordText())
        {
            if (reachPointTextController.CanDisplayNewRecord())
            {
                StartCoroutine(ShowText(text));
            }
            else
            {
                ExecuteAfterShowTextIsComplete();
            }
        }
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        reachPointTextController.AnimateRecordText();
    }
}

