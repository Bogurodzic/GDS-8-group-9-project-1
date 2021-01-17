using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class Rock : Obstacle
{

    private bool _destroyInitialised = false;
    private bool _rockDestroyed = false;
   // private UnityArmatureComponent _rockAnimation;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        /*
        if (_destroyInitialised && !_rockDestroyed)
        {
            if (_rockAnimation.animation.isCompleted)
            {
                _rockDestroyed = true;
            }
        } 
        if (_destroyInitialised && _rockDestroyed)
        {
            Destroy(gameObject);
        }
        */
    }

    private void LoadComponents()
    {
       // _rockAnimation = GetComponent<UnityArmatureComponent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (!_destroyInitialised)
            {
                HandleDestroyingRock();
            }
        }
    }

    private void HandleDestroyingRock()
    {
        _destroyInitialised = true;
       // _rockAnimation.animation.Play("boulder_destroy_small", 1);
        _boxCollider2D.enabled = false;
        _rigidbody2D.Sleep();
        Destroy(gameObject);
    }
}
