using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class Bomb : MonoBehaviour
{

    public float bombFireForce;

    private Rigidbody2D _bombRigidBody;

    public enum BombDirection
    {
        Left,
        Right
    }

    void Start()
    {
        
    }

    public void StartObject(BombDirection bombDirection)
    {
        GameManager.Instance.BombDeployed();
        LoadComponents();
        _bombRigidBody.AddForce(new Vector2(GetBombDirection(bombDirection)  * (bombFireForce / 2), 1 * bombFireForce), ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }

    private void LoadComponents()
    {
        _bombRigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void DestroyBomb()
    {
        GameManager.Instance.BombDestroyed();
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Projectile"))
        {
            DestroyBomb();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            DestroyBomb();
        }

    }
    
    private int GetBombDirection(BombDirection bombDirection)
    {
        if (bombDirection == BombDirection.Left)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}
