﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    protected Progress progress;
    protected AudioSource _audio;
    protected void Start()
    {
        LoadComponents();
        Debug.Log("CHECKPOINT GAME OBJECT NAME: " +gameObject.name);
    }

    void Update()
    {
        
    }
    

    protected void LoadComponents()
    {
        progress = GameObject.Find("LandWrapper").GetComponent<Progress>();
        _audio = GetComponent<AudioSource>();
    }

    protected virtual void OnCheckpointPassed()
    {
        UpdateLastCheckpointProgress();
        if (_audio)
        {
            _audio.Play();

        }
    }
    

    protected void UpdateLastCheckpointProgress()
    {
        GameManager.Instance.SetLastProgressCheckpoint(GameManager.Instance.GetProgress());
    }
    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCheckpointPassed();
        }
    }
}
