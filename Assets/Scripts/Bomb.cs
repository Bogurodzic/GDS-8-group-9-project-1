using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DragonBones;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class Bomb : MonoBehaviour
{

    public float bombFireForce;
    private Rigidbody2D _bombRigidBody;
    private UnityArmatureComponent _bombAnimation;
    private bool _destroyInitialised = false;
    public enum BombDirection
    {
        Left,
        Right
    }

    void Start()
    {
        LoadComponents();
    }

    public void StartObject(BombDirection bombDirection)
    {
        LoadComponents();
        _bombAnimation.animation.Play("bomb_idle", 2);
        GameManager.Instance.BombDeployed();
        _bombRigidBody.AddForce(new Vector2(GetBombDirection(bombDirection)  * (bombFireForce / 2), 1 * bombFireForce), ForceMode2D.Impulse);


    }

    void Update()
    {
        if (_bombAnimation.animation.isCompleted && !_destroyInitialised)
        {
            _bombAnimation.animation.Play("bomb_fly", 1);
        } else if (_destroyInitialised)
        {
            if (_bombAnimation.animation.isCompleted)
            {
                Destroy(gameObject);
            }
        }
    }

    private void LoadComponents()
    {
        _bombRigidBody = GetComponent<Rigidbody2D>();
        _bombAnimation = GetComponent<UnityArmatureComponent>();
    }
    
    private void DestroyBomb()
    {
        GameManager.Instance.BombDestroyed();

        _destroyInitialised = true;
        _bombAnimation.animation.Play("bomb_explosion", 1);
       // _boxCollider2D.enabled = false;
        _bombRigidBody.Sleep();
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        
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
