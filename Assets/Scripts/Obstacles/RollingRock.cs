using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : Rock
{
    // Start is called before the first frame update
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
}
