using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float speed;

    private bool _destroyInitialised;
    private bool _tankDestroyed;
    private UnityArmatureComponent _tankAnimation;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        
        
        if (_destroyInitialised && !_tankDestroyed)
        {
            if (_tankAnimation.animation.isCompleted)
            {
                _tankDestroyed = true;
            }
        } 
        if (_destroyInitialised && _tankDestroyed)
        {
            Destroy(gameObject);
        }

    }
    
    private void LoadComponents()
    {
        _tankAnimation = GetComponent<UnityArmatureComponent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    
    
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Projectile"))
        {
            HandleCollisionWithProjectile();
        }

    }
    
    private void HandleCollisionWithProjectile()
    {
        _destroyInitialised = true;
        _tankAnimation.animation.Play("enemy_death", 1);
        _boxCollider2D.enabled = false;
        _rigidbody2D.Sleep();
        Destroy(gameObject);
    }
}
