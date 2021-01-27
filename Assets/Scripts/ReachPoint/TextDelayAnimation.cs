using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextDelayAnimation : MonoBehaviour
{
    public float delay = 0.05f;
    public string fullText;
    
    protected TextMeshProUGUI _text;
    protected bool showTextStarted = false;
    protected ReachPointTextController reachPointTextController;
    
    private string _currentText = "";
    protected void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowText(_text));
    }

    void Update()
    {
        
    }

    protected virtual void ExecuteAfterShowTextIsComplete()
    {
    }

    protected IEnumerator ShowText(TextMeshProUGUI text)
    {
        showTextStarted = true;
        
        for (int i = 0; i <= fullText.Length; i++)
        {
            _currentText = fullText.Substring(0, i);
            text.text = _currentText;
            
            if (i == fullText.Length)
            {
                ExecuteAfterShowTextIsComplete();
            }
            
            
            yield return new WaitForSeconds(delay);
        }
    }
}
