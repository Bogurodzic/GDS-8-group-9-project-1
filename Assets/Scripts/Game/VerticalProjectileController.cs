using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class VerticalProjectileController : ProjectileController
{

    private bool _initialAnimationPlayed = false;
    private bool _initProjectileDestroy = false;
    private bool _projectileDestroyAnimationPlayed = false;
    private UnityArmatureComponent _projectileAnimation;
    
    void Update()
    {
        if (ProjectileIsAlive())
        {
            HandleProjectileTrajectory();
        }
        HandleProjectileDestroy();
    }

    private bool ProjectileIsAlive()
    {
        return !_initProjectileDestroy;
    }

    private void HandleProjectileDestroy()
    {
        if (_initialAnimationPlayed && !_initProjectileDestroy)
        {
            if (IsCurrentAnimationCompleted())
            {
                _projectileAnimation.animation.Play("rocket_idle");
            }
        } else if (_initProjectileDestroy && !_projectileDestroyAnimationPlayed)
        {
            _projectileAnimation.animation.Play("rocket_explosion", 1);
            _projectileDestroyAnimationPlayed = true;
        } else if (_projectileDestroyAnimationPlayed)
        {
            if (IsCurrentAnimationCompleted()) 
            {
                DestroyProjectile();
            }
        }
    }

    private bool IsCurrentAnimationCompleted()
    {
        return _projectileAnimation.animation.isCompleted;
    }
    
    public void PlayAnimationAccordingToPlayerAnimation(PlayerController.PlayerAnimation playerAnimation)
    {
        
        _projectileAnimation = gameObject.GetComponent<UnityArmatureComponent>();

        switch (playerAnimation)
        {
            case PlayerController.PlayerAnimation.Acceleration:
                _projectileAnimation.animation.Play("rocket_drive_fast", 1);
                break;
            default:
                _projectileAnimation.animation.Play("rocket_drive_idle", 1);
                break;
        }
        
        _initialAnimationPlayed = true;
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
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
    
    
    public override void InitDestroyProjectile()
    {
        _initProjectileDestroy = true;
    }

}
