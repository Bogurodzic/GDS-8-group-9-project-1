using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController : MonoBehaviour
{
    public float landSpeed;
    private float _initLandSpeed;
    private float _startingHorizontalPosition;
    
    private BoxCollider2D _boxCollider;
    void Start()
    {
        LoadComponents();
        _startingHorizontalPosition = transform.position.x;
        _initLandSpeed = landSpeed;
    }
    void Update()
    {
        if (GameManager.Instance.IsGameRunning())
        {
            UpdateGameSpeed();
            MoveLand();
            if (_boxCollider)
            {
                _boxCollider.enabled = true;
            }
        }
        else
        {
            if (_boxCollider)
            {
                _boxCollider.enabled = false;
            }
        }
    }

    private void UpdateGameSpeed()
    {
        if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Normal)
        {
            landSpeed = _initLandSpeed;
        }
        else if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Fast)
        {
            landSpeed = _initLandSpeed * 1.3f;
        } else if (GameManager.Instance.GetPlayerSpeed() == GameManager.PlayerSpeed.Slow)
        {
            landSpeed = _initLandSpeed * 0.7f;
        }
    }

    private void LoadComponents()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void MoveLand()
    {
        transform.Translate(Vector3.left * landSpeed * Time.deltaTime);
    }

    private void HandleRepeatingBackground()
    {
        if (transform.position.x < (_boxCollider.size.x / 2) * -1)
        {
            transform.position = new Vector3(_startingHorizontalPosition, transform.position.y, transform.position.z);
        }
    }
}
