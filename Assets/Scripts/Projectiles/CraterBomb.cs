using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = System.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


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
        if (!_destroyInitialised)
        {

        }
        else
        {
            transform.Translate(Vector3.left * 4.5f * Time.deltaTime);
            transform.rotation = Quaternion.identity;
        }

    }

    private void FixedUpdate()
    {

        if (transform.rotation.z > 0)
        {
            transform.Rotate(Vector3.back * 1.1f);
        }

    }

    protected override void AddInitialForceToBomb(BombDirection bombDirection)
    {   
        Debug.Log("START BOMB CRATER");
        float fixedBombFireForce = bombFireForce;
        if (transform.position.x < -14)
        {
            fixedBombFireForce = fixedBombFireForce * 1.6f;
        }
        _bombRigidBody.AddForce(new Vector2(GetBombDirection(bombDirection)  * (fixedBombFireForce), 1), ForceMode2D.Impulse);

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
        _bombAnimation.animation.timeScale = 2f;
        _bombAnimation.animation.Play("bomb_explosion", 1);
        _boxCollider2D.enabled = false;
        _bombRigidBody.Sleep();
    }
}
