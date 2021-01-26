using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public string phase1Text;
    public string phase2Text;
    public string phase3Text;
    public string phase4Text;

    public float phaseDelay;
    
    private Text _coutdownText;
    private AudioSource _audio;
    void Start()
    {
        LoadComponents();
        BeginCountdown();
    }

    void Update()
    {
        
    }

    private void LoadComponents()
    {
        _coutdownText = GetComponent<Text>();
        _audio = GetComponent<AudioSource>();
    }

    private void BeginCountdown()
    {
        CountdownPhase1();
        Invoke("CountdownPhase2", phaseDelay);
        Invoke("CountdownPhase3", phaseDelay * 2);
        Invoke("CountdownPhase4", phaseDelay * 3);
        Invoke("TurnOffCountdown", phaseDelay * 4);

    }

    private void CountdownPhase1()
    {
        _audio.Play();
        _coutdownText.text = phase1Text;
    }
    
    private void CountdownPhase2()
    {
        _audio.Play();
        _coutdownText.text = phase2Text;
    }
    
    private void CountdownPhase3()
    {
        _audio.Play();
        _coutdownText.text = phase3Text;
    }
    
    private void CountdownPhase4()
    {
        _audio.Play();
        _coutdownText.text = phase4Text;
        GameManager.Instance.StartGame();
    }

    private void TurnOffCountdown()
    {
        Destroy(gameObject);
    }
}
