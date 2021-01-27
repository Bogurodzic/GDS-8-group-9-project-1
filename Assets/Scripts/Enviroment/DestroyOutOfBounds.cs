using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Enemy enemy = GetComponent<Enemy>();
        
        if (transform.position.x < -35)
        {
            if (enemy)
            {
                enemy.Destroy();
            }
            else
            {
                Destroy(gameObject);
            }
        } else if (transform.position.y > 10)
        {
            if (enemy)
            {
                enemy.Destroy();
            }
            else
            {
                Destroy(gameObject);
            }
        } else if (transform.position.x > 15)
        {
            if (enemy)
            {
                enemy.Destroy();
            }
            else
            {
                Destroy(gameObject);
            }
            
        } else if (transform.position.y < -10)
        {
            if (enemy)
            {
                enemy.Destroy();
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }
}
