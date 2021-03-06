﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YourTime : TextDelayAnimation
{
    private TextMeshProUGUI _yourTime;
    void Start()
    {
        LoadComponents();
        fullText = string.Format("{0, 3:000}", GameManager.Instance.GetTimer());
    }

    private void LoadComponents()
    {
        _yourTime = GetComponent<TextMeshProUGUI>();
        reachPointTextController = GameObject.Find("ReachPoint").GetComponent<ReachPointTextController>();
    }

    void Update()
    {
        if (!showTextStarted && reachPointTextController.CanAnimateYourTimeScore())
        {
            StartCoroutine(ShowText(_yourTime));
        }
    }

    protected override void ExecuteAfterShowTextIsComplete()
    {
        reachPointTextController.AnimateYourTimeScore();
    }
}
