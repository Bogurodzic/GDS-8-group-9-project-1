using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRockBig : RollingRock
{
    void Start()
    {
        LoadComponents();
    }

    // Update is called once per frame
    void Update()
    {
        HandleDestroyingRock();
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollisionWithProjectile(collision);
    }
    
    protected override void InitDestroyRock()
    {
        _destroyInitialised = true;
        _rockAnimation.animation.Play("boulder_destroy_big", 1);
        _boxCollider2D.enabled = false;
        _rigidbody2D.Sleep();
    }
}
