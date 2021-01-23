using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    protected Progress progress;
    protected void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        
    }
    

    protected void LoadComponents()
    {
        progress = GameObject.Find("LandWrapper").GetComponent<Progress>();
    }

    protected virtual void OnCheckpointPassed()
    {
        UpdateLastCheckpointProgress();
        Debug.Log("UpdateLastCheckpointPosition: " + progress.GetCurrentPosition());
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
