using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AverageTime : MonoBehaviour
{
    private TextMeshProUGUI _averageTime;
    void Start()
    {
        _averageTime = GetComponent<TextMeshProUGUI>();
        _averageTime.text = string.Format("{0, 3:000}", GameManager.Instance.GetAverageTime(StageManager.Instance.GetCurrentStage()));
    }

    void Update()
    {
        
    }
}
