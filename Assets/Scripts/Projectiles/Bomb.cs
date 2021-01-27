using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DragonBones;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Bomb : MonoBehaviour
{

    public float bombFireForce;
    protected Rigidbody2D _bombRigidBody;
    protected BoxCollider2D _boxCollider2D;
    protected UnityArmatureComponent _bombAnimation;
    protected bool _destroyInitialised = false;
    protected BombDirection _bombDirection;
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
        _bombAnimation.animation.Play("bomb_fly", 2);
        BombDeployed();
        AddInitialForceToBomb(bombDirection);
    }

    protected virtual void AddInitialForceToBomb(BombDirection bombDirection)
    {
        _bombDirection = bombDirection;
        _bombRigidBody.AddForce(new Vector2(GetBombDirection(bombDirection)  * (bombFireForce / 2), 1 * bombFireForce), ForceMode2D.Impulse);

    }

    protected virtual void BombDeployed()
    {
        GameManager.Instance.BombDeployed();
    }

    void Update()
    {
        HandleBombDestroy();
    }
    
    private void FixedUpdate()
    {

        if (_bombDirection == BombDirection.Right)
        {
            if (transform.rotation.z > 0)
            {
                transform.Rotate(Vector3.back * 1.1f);
            } 
        } else if (_bombDirection == BombDirection.Left)
        {
            if (transform.rotation.z < 0)
            {
                transform.Rotate(Vector3.forward * 1.1f);
            } 
        }


    }

    protected void HandleBombDestroy()
    {
        if (_bombAnimation.animation.isCompleted && !_destroyInitialised)
        {
            _bombAnimation.animation.Play("bomb_idle", 1);
        } else if (_destroyInitialised)
        {
            if (_bombAnimation.animation.isCompleted)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void LoadComponents()
    {
        _bombRigidBody = GetComponent<Rigidbody2D>();
        _bombAnimation = GetComponent<UnityArmatureComponent>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    protected virtual void DestroyBomb()
    {
        GameManager.Instance.BombDestroyed();

        _destroyInitialised = true;
        _bombAnimation.animation.timeScale = 2f;
        _bombAnimation.animation.Play("bomb_explosion", 1);
        _boxCollider2D.enabled = false;
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
    
    protected int GetBombDirection(BombDirection bombDirection)
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
