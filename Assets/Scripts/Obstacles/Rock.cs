using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class Rock : Obstacle
{

    public int pointsForKill;
    protected bool _destroyInitialised = false;
    protected bool _rockDestroyed = false;
    protected UnityArmatureComponent _rockAnimation;
    protected BoxCollider2D _boxCollider2D;
    protected Rigidbody2D _rigidbody2D;
    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        HandleDestroyingRock();
    }

    protected void HandleDestroyingRock()
    {
        if (_destroyInitialised && !_rockDestroyed)
        {
            Debug.Log("Crystal INIT 2");

            if (_rockAnimation.animation.isCompleted)
            {
                Debug.Log("Crystal INIT 3");

                _rockDestroyed = true;
            }
        } 
        if (_destroyInitialised && _rockDestroyed)
        {
            Debug.Log("Crystal INIT 4");

            Destroy(gameObject);
        }     
    }

    protected void LoadComponents()
    {
        _rockAnimation = GetComponent<UnityArmatureComponent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollisionWithProjectile(collision);
    }

    protected void HandleCollisionWithProjectile(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (!_destroyInitialised)
            {
                InitDestroyRock();
                Destroy(collision.gameObject);
            }
        }
    }

    public virtual void InitDestroyRock()
    {
        gameObject.tag = "Untagged";
        GameManager.Instance.AddPoints(pointsForKill);
        _destroyInitialised = true;
        _rockAnimation.animation.Play("crystal_death", 1);
        Debug.Log("Crystal INIT 1");
    }
}
