using System;
using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using System.Threading.Tasks;

public class Tank : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float speed;
    public int projectileInterval;
    public float distanceBetweenPlayerBeforeStartShooting;

    public int pointsForKill;
    
    private bool _destroyInitialised;
    private bool _tankDestroyed;
    private bool _shootReady = true;
    private UnityArmatureComponent _tankAnimation;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;

    private GameObject _player;
    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        HandleShooting();
        
        
        
        if (_destroyInitialised && !_tankDestroyed)
        {
            if (_tankAnimation.animation.isCompleted)
            {
                _tankDestroyed = true;
            }
        } else if (_destroyInitialised && _tankDestroyed)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

    }
    
    private void HandleShooting()
    {

        if (ShouldEnemyShoot() && IsTankNearPlayer()) 
        {
            CreateProjectile(); 
            ReloadShoot();
        }

    }
    
    private bool ShouldEnemyShoot(){
        return ( _shootReady);
    }

    private void CreateProjectile()
    {
        Instantiate(projectilePrefab, new Vector3(transform.position.x + 1.1f, transform.position.y + 0.05f, transform.position.z), projectilePrefab.transform.rotation);
    }
    
    private void ReloadShoot()
    {
        DisableHorizontalShoot();
        Task.Delay(projectileInterval * 1000).ContinueWith(t=> EnableHorizontalShoot());
    }
    
    private void DisableHorizontalShoot()
    {
        _shootReady = false;
    }
    private void EnableHorizontalShoot()
    {
        _shootReady = true;
    }

    
    private void LoadComponents()
    {
        _tankAnimation = GetComponent<UnityArmatureComponent>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _player = GameObject.Find("Car");
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
        DestroyTank();
    }

    public void DestroyTank()
    {
        GameManager.Instance.AddPoints(pointsForKill);
        _destroyInitialised = true;
        _tankAnimation.animation.Play("enemy_death", 1);
        _boxCollider2D.enabled = false;
        _rigidbody2D.Sleep();
    }

    private bool IsTankNearPlayer()
    {
        return Math.Abs(_player.transform.position.x) - Math.Abs(gameObject.transform.position.x) <=
               distanceBetweenPlayerBeforeStartShooting;
    }
    
}
