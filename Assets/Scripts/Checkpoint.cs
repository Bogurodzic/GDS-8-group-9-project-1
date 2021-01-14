using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public static float LastCheckpointXPoint;

    private Progress _progress;

    public float checkpointIndex;
    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        
    }

    private void LoadComponents()
    {
        _progress = GameObject.Find("LandWrapper").GetComponent<Progress>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LastCheckpointXPoint = _progress.GetCurrentPosition();
        }
    }
}
