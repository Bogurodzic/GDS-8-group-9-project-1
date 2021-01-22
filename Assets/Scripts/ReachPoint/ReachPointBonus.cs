using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReachPointBonus : MonoBehaviour
{
    private TextMeshProUGUI _bonusPointsText;
    public int baseBonusPoints = 1000;
    void Start()
    {
        _bonusPointsText = GetComponent<TextMeshProUGUI>();
        _bonusPointsText.text = GetBonusPoint().ToString();
    }

    void Update()
    {
        
    }

    private int GetBonusPoint()
    {
        if (GameManager.Instance.GetTimer() <
            GameManager.Instance.GetAverageTime(StageManager.Instance.GetCurrentStage()))
        {
            int timeDifference =   (int) (GameManager.Instance.GetAverageTime(StageManager.Instance.GetCurrentStage()) -
                                          GameManager.Instance.GetTimer());
            return baseBonusPoints + (timeDifference * 100);
        }
        else
        {
            return baseBonusPoints;
        }
    }
}
