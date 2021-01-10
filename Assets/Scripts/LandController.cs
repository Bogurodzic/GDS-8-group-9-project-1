using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandController : MonoBehaviour
{
    public float landSpeed;
    private float _startingHorizontalPosition;
    
    private BoxCollider2D _boxCollider;
    void Start()
    {
        LoadComponents();
        _startingHorizontalPosition = transform.position.x;
    }
    void Update()
    {
        if (GameManager.Instance.IsGameRunning())
        {
            MoveLand();
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
