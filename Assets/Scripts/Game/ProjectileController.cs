using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Vertical,
        Right
    }       

    public Direction projectileDirection;
    public float projectileSpeed;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        HandleProjectileTrajectory();
        if (projectileDirection == Direction.Right && transform.position.x > -10.5)
        {
            Destroy(gameObject);
        }
    }

    private void HandleProjectileTrajectory()
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
    
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (projectileDirection == Direction.Left)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }        
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
        Debug.Log("DESTROYED");

    }
}
