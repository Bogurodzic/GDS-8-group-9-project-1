using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextPoint : MonoBehaviour
{
    public TextMeshProUGUI nextReachPoint;
    void Start()
    {
        nextReachPoint = GetComponent<TextMeshProUGUI>();
        nextReachPoint.text = "Time to reach point '" + (StageManager.Instance.GetCurrentStage() + 1) + "'";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
