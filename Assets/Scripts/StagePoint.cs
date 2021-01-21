using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StagePoint : Checkpoint
{
    
    protected override void OnCheckpointPassed()
    {
        //UpdateLastCheckpointPosition();
        Debug.Log("STAGE CLEARED ELELELELLEELEL ONEONEONEOENOE");
        SceneManager.LoadScene("Scenes/ReachPointScreen");
    }
    
    
}
