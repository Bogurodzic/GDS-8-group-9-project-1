using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class StagePoint : Checkpoint
{
    private bool _stagePointReached;
    
    protected override void OnCheckpointPassed()
    {
        _audio.Play();
        _stagePointReached = true;
        GameManager.Instance.StopGame();
        GameManager.Instance.PauseGame();

    }
    
    void Update()
    {
        if (!_audio.isPlaying && _stagePointReached)
        {
            SceneManager.LoadScene("Scenes/ReachPointScreen");
            GameManager.Instance.StartGame();
            GameManager.Instance.PlayGame();
        }
    }

}
