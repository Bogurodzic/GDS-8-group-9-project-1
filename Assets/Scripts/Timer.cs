using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public float time;
    float sec;
    
    private void Start() {
        StartCoroutine("StopWatch");
    }
    IEnumerator StopWatch(){
        while(true){
            time += Time.deltaTime;
            sec = (int)(time % 60);

            timer.text = string.Format("time {0, 3:000}", sec);



            yield return null;
        }
    }
}
