using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DragonBones;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public enum Direction
    {
        Left,
        Vertical,
        Right
    }       

    public Direction projectileDirection;
    public float projectileSpeed;
    
    void Start()
    {
        LoadComponents();
    }

    private void LoadComponents()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        HandleProjectileTrajectory();
        
        if (projectileDirection == Direction.Right && transform.position.x > -10.5)
        {
            Destroy(gameObject);
        }
    }
    
    protected void HandleProjectileTrajectory()
    {
        if (projectileDirection == Direction.Right)
        {
            MoveHorizontallyRight();
        } else if (projectileDirection == Direction.Vertical)
        {   
            MoveVertically();
        } else if (projectileDirection == Direction.Left)
        {
            MoveHorizontallyleft();
        }
    }

    private void MoveHorizontallyRight() {
        gameObject.transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }
    
    private void MoveHorizontallyleft() {
        gameObject.transform.Translate(Vector3.left * projectileSpeed * Time.deltaTime);
    }

    private void MoveVertically()
    {
        gameObject.transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);

    }
    
    
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (projectileDirection == Direction.Left)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                InitDestroyProjectile();
                Destroy(collision.gameObject);
            }        
        }

    }
    
    public virtual void InitDestroyProjectile()
    {
        DestroyProjectile();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
