using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float timeStart;
    public Text textBox;
    //public Text startBtnText;

    bool timerActive = false;

    // Use this for initialization
    void Start () {
        timer.text = timeStart.ToString("F2");
        timerActive = true;
    }
	
    // Update is called once per frame
    void Update () {
        if(timerActive)
        {
            float timePassed = GameManager.Instance.GetTimer() + Time.deltaTime;
            GameManager.Instance.SetTimer(timePassed);
            timer.text = string.Format("time {0, 3:000}", GameManager.Instance.GetTimer());
        }
    }
    public void timerButton(){
        timerActive = !timerActive;
        //startBtnText.text = timerActive ? "Pause" : "Start";
    }
    
    
    
}
