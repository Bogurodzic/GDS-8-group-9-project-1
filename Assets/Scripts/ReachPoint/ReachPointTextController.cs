using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachPointTextController : MonoBehaviour
{

    private bool _mainTextCompleted = false;
    
    private bool _yourTimeTextCompleted = false;
    private bool _yourTimeScoreCompleted = false;

    private bool _averageTimeTextCompleted = false;
    private bool _averageTimeScoreCompleted = false;

    private bool _topRecordTextCompleted = false;
    private bool _topRectordScoreCompleted = false;

    private bool _specialBonusTextCompleted = false;
    private bool _specialBonusScoreCompleted = false;

    private bool _recordTextCompleted = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool CanAnimateMainText()
    {
        return !_mainTextCompleted;
    }

    public void AnimateMainText()
    {
        _mainTextCompleted = true;
    }

    public bool CanAnimateYourTimeText()
    {
        if (_mainTextCompleted && !_yourTimeTextCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AnimateYourTimeText()
    {
        _yourTimeTextCompleted = true;
    }
    
    public bool CanAnimateYourTimeScore()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && !_yourTimeScoreCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void AnimateYourTimeScore()
    {
        _yourTimeScoreCompleted = true;
    }
    
    public bool CanAnimateAverageTimeText()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && _yourTimeScoreCompleted && !_averageTimeTextCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void AnimateAverageTimeText()
    {
        _averageTimeTextCompleted = true;
    }
    
    public bool CanAnimateAverageTimeScore()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && _yourTimeScoreCompleted && _averageTimeTextCompleted && !_averageTimeScoreCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void AnimateAverageTimeScore()
    {
        _averageTimeScoreCompleted = true;
    }
    
    public bool CanAnimateYourTopRecordText()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && _yourTimeScoreCompleted && _averageTimeTextCompleted && _averageTimeScoreCompleted && !_topRecordTextCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void AnimateYourTopRecordText()
    {
        _topRecordTextCompleted = true;
    }
    
    public bool CanAnimateYourTopRecordScore()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && _yourTimeScoreCompleted && _averageTimeTextCompleted && _averageTimeScoreCompleted && _topRecordTextCompleted && !_topRectordScoreCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void AnimateYourTopRecordScore()
    {
        _topRectordScoreCompleted = true;
    }
    
    public bool CanAnimateSpecialBonusText()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && _yourTimeScoreCompleted && _averageTimeTextCompleted && _averageTimeScoreCompleted && _topRecordTextCompleted && _topRectordScoreCompleted && !_specialBonusTextCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool CanAnimateSpecialBonusScore()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && _yourTimeScoreCompleted && _averageTimeTextCompleted && _averageTimeScoreCompleted && _topRecordTextCompleted && _topRectordScoreCompleted && _specialBonusTextCompleted && !_specialBonusScoreCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool CanAnimateRecordText()
    {
        if (_mainTextCompleted && _yourTimeTextCompleted && _yourTimeScoreCompleted && _averageTimeTextCompleted && _averageTimeScoreCompleted && _topRecordTextCompleted && _topRectordScoreCompleted && _specialBonusTextCompleted && _specialBonusScoreCompleted && !_recordTextCompleted)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
