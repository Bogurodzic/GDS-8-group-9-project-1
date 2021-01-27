using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CautionPoint : MonoBehaviour
{

    public CautionType pointType;
    public enum CautionType
    {
        Air,
        Land,
        Obstacle
    }

    private CautionController _cautionController;

    
    void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _cautionController = GameObject.Find("Caution").GetComponent<CautionController>();
    }
    
    void Update()
    {
        
    }
    
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnCautionPointPassed();
        }

    }


    private void OnCautionPointPassed()
    {
        if (pointType == CautionType.Air)
        {
            _cautionController.ActiveAirFlag();   
        } else if (pointType == CautionType.Land)
        {
            _cautionController.ActiveLandFlag();
        } else if (pointType == CautionType.Obstacle)
        {
            _cautionController.ActiveObstacleFlag();
        }
    }
    

}
