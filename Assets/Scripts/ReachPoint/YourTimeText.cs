using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YourTimeText : TextDelayAnimation
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
        if (!showTextStarted && reachPointTextController.CanAnimateYourTimeText())
        {
            StartCoroutine(ShowText(text));
        }
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        reachPointTextController.AnimateYourTimeText();
    }
}
