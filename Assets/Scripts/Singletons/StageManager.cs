using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : GenericSingletonClass<StageManager>
{
    private int _currentStage = 1;
    private int _lastStage = 5;
    void Start()
    {
        
    }
          
    void Update()
    {
        
    }

    public int GetCurrentStage()
    {
        return _currentStage;
    }

    public int GetLastStage()
    {
        return _lastStage;
    }
}
