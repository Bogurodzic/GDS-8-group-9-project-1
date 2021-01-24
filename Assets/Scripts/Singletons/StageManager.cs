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

    public void NextStage()
    {
        if (_currentStage < _lastStage)
        {
            _currentStage++;
            GameManager.Instance.SetTimer(0);
        }
    }

    public bool IsCurrentStageLast()
    {
        return !(_currentStage < _lastStage);
    }
}
