using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private UnityEngine.UI.Text txt;
    void Start()
    {
        txt = GetComponent<UnityEngine.UI.Text>();

    }

    void Update()
    {
        txt.text = "High Score: " + GameManager.Instance.GetHighScore().ToString();
    }
}