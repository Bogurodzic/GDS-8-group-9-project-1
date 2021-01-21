using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopTime : MonoBehaviour
{    
    private TextMeshProUGUI _topTime;
    void Start()
    {
        _topTime = GetComponent<TextMeshProUGUI>();
        _topTime.text = string.Format("{0, 3:000}", GameManager.Instance.GetTopRecord(StageManager.Instance.GetCurrentStage()));
    }

    void Update()
    {
        
    }
}
