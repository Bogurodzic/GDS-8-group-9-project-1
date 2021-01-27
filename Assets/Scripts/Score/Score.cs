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

    // Update is called once per frame
    void Update()
    {
        txt.text = GameManager.Instance.GetCurrentScore().ToString();
    }
}
