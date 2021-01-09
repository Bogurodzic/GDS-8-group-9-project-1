using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float bombSpeed;
    public float bombDelaySpeed;
    
    private Vector3 _basicBombTargetPosition;
    
    void Start()
    {
        GameManager.Instance.BombDeployed();
        LoadComponents();
        Invoke("SpeedUpBomb", 1.0f);
    }

    void Update()
    {
        Drop();
    }

    private void LoadComponents()
    {
    }
    private void Drop() {
        transform.Translate(Vector3.down * bombSpeed * Time.deltaTime);
    }
    
    private void SpeedUpBomb()
    {
        bombSpeed = bombDelaySpeed;
    }

    private void DestroyBomb()
    {
        GameManager.Instance.BombDestroyed();
        Destroy(gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log(collision.gameObject.tag);
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
}
