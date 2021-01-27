using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : Rock
{
    protected CircleCollider2D _circleCollider2D;
    void Start()
    {
        LoadComponents();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        HandleDestroyingRock();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollisionWithProjectile(collision);
    }
    
    public override void InitDestroyRock()
    {
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        _circleCollider2D.enabled = false;
        _destroyInitialised = true;
        _rockAnimation.animation.Play("boulder_destroy_small", 1);
  
    }
}
