using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOverObstacle : MonoBehaviour
{
    public int pointsForJump;
    private bool _pointAcquired = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        HandleJumpingOverObstacle(collision);
    }

    public void HandleJumpingOverObstacle(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandleAddingPointsToPlayer();
        }
    }

    private void HandleAddingPointsToPlayer()
    {
        if (CanAddPoints())
        {
            GameManager.Instance.AddPoints(pointsForJump);
            _pointAcquired = true;
        }
    }

    private bool CanAddPoints()
    {
        return GameManager.Instance.IsGameRunning() && !_pointAcquired;
    }
}
