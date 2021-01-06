using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoints : MonoBehaviour
{
    public int bonusPointFor3Stack;
    public int bonusPointFor4Stack;
    public int bonusPointFor5Stack;

    private LinkedList<EnemyStackStatus> _enemyStackStatuses = new LinkedList<EnemyStackStatus>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddStack(string enemyStackID, int quantity)
    {
        _enemyStackStatuses.AddLast(new EnemyStackStatus(enemyStackID, quantity));
    }

    public void DecreaseStackByOne(string enemyStackID)
    {
        foreach (EnemyStackStatus status in _enemyStackStatuses)
        {
            if (status.EnemyStackID == enemyStackID)
            {
                status.DecreaseByOne();
                CheckIfStackIsEligibleForBonusPoints(status);
            }
        }
    }

    private void CheckIfStackIsEligibleForBonusPoints(EnemyStackStatus status)
    {
        if (status.Quantity == 0)
        {
            AddBonusPointsAccordingToStackSize(status.InitialQuantity);
        }
    }

    private void AddBonusPointsAccordingToStackSize(int stackSize)
    {
        switch (stackSize)
        {
            case 3:
                GameManager.Instance.AddPoints(bonusPointFor3Stack);
                break;
            case 4:
                GameManager.Instance.AddPoints(bonusPointFor4Stack);
                break;
            case 5:
                GameManager.Instance.AddPoints(bonusPointFor5Stack);
                break;
        } 
    }
    

    
}


public class EnemyStackStatus
{
    public EnemyStackStatus(string enemyStackID, int quantity)
    {
        EnemyStackID = enemyStackID;
        Quantity = quantity;
        InitialQuantity = quantity;
    }

    public string EnemyStackID { get; }
    public int Quantity { get; set; }
    public int InitialQuantity { get; }


    public void DecreaseByOne()
    {
        Quantity -= 1;
    }
    
}

