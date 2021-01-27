using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    private UnityEngine.UI.Text txt;
    void Start()
    {
        txt = GetComponent<UnityEngine.UI.Text>();

    }

    // Update is called once per frame
    void Update()
    {
        txt.text = GameManager.Instance.GetPlayerLives().ToString();
    }
}
