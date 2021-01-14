using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePoint : Checkpoint
{
    
    protected override void OnCheckpointPassed()
    {
        UpdateLastCheckpointPosition();
        Debug.Log("STAGEPOINT: " + LastCheckpointXPoint);
    }
    
    
}
