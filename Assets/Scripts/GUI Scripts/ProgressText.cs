using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressText : MonoBehaviour
{
    private UnityEngine.UI.Text txt;
    void Start()
    {
        txt = GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        txt.text = "Progress: " + GameManager.Instance.GetProgress().ToString() + "%";

    }
}
