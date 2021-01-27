using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private UnityEngine.UI.Text txt;
    void Start()
    {
        txt = GetComponent<UnityEngine.UI.Text>();

    }

    void Update()
    {
        txt.text = "Score: " + GameManager.Instance.GetCurrentScore().ToString();
    }
}
