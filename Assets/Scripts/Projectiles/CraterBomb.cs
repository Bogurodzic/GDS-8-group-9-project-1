using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class CraterBomb : Bomb
{

    public GameObject[] holePrefabs;
    void Start()
    {
        LoadComponents();
    }

    void Update()
    {
        HandleBombDestroy();
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-9.55f, -1.15f, 0 ), 1.6f * Time.deltaTime);

    }
    
    protected override void BombDeployed()
    {
        GameManager.Instance.CraterBombDemployed();
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
            CreateCrater();
        }

    }
    
    private void CreateCrater()
    {
        Random random = new Random();
        GameObject holePrefab = holePrefabs[random.Next(1, holePrefabs.Length)];
        Instantiate(holePrefab, new Vector3(transform.position.x, -1.13f, transform.position.z), transform.rotation);
    }
    
    protected virtual void DestroyBomb()
    {
        GameManager.Instance.CraterBombDestroyed();
        _destroyInitialised = true;
        _bombAnimation.animation.Play("bomb_explosion", 1);
        _boxCollider2D.enabled = false;
        _bombRigidBody.Sleep();
    }
}
