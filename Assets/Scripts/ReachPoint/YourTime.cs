using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YourTime : MonoBehaviour
{
    private TextMeshProUGUI _yourTime;
    void Start()
    {
        _yourTime = GetComponent<TextMeshProUGUI>();
        _yourTime.text = string.Format("{0, 3:000}", GameManager.Instance.GetTimer());
    }

    void Update()
    {
        
    }
}
