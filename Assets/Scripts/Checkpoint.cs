using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public static float LastCheckpointXPoint;

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
        UpdateLastCheckpointPosition();
    }
    

    protected void UpdateLastCheckpointPosition()
    {
        LastCheckpointXPoint = progress.GetCurrentPosition();
    }
    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCheckpointPassed();
        }
    }
}
