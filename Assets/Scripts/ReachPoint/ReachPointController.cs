﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReachPointController : MonoBehaviour
{
    private ReachPointTextController _reachPointTextController;
    private AudioSource _audio;
    void Start()
    {
        LoadComponents();

        if (StageManager.Instance.IsCurrentStageLast())
        {
            _audio.Play();
        }
    }

    private void LoadComponents()
    {
        _reachPointTextController = GetComponent<ReachPointTextController>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (CanProgressToNextStage())
            {
                StageManager.Instance.NextStage();
                SceneManager.LoadScene("Scenes/Levels/Level" + StageManager.Instance.GetCurrentStage());
            }

        }
    }

    private bool CanProgressToNextStage()
    {
        return !StageManager.Instance.IsCurrentStageLast() &&
               _reachPointTextController.CanGoToNewStage();
    }
}
