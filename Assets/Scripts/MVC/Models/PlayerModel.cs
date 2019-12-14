using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// MVC Model for Player.
/// Has Projectile speed, jump force multiplier, score.
/// Has Object Pooling implemented for projectiles.
/// </summary>
public class PlayerModel : CharacterModel
{
    public float _projectileSpeed;
    public float _jumpForceMultiplier;
    public int score { get; set; }
    private List<Projectile> _projectilePool;

    /// <summary>
    /// Checks first projectile that is inactive(unused) and passes that to PlayerController for shooting.
    /// </summary>
    /// <returns></returns>
    public Projectile GetNextValidProjectile ( )
    {
        return _projectilePool.Find ( x => x.gameObject.activeInHierarchy == false );
    }

    /// <summary>
    /// Pool initialized from PlayerController.
    /// </summary>
    /// <param name="projectile"></param>
    /// <param name="poolSize"></param>
    public void InitializeProjectilePool ( Projectile projectile, int poolSize )
    {
        _projectilePool = new List<Projectile> ( poolSize );
        for ( int i = 0; i < poolSize; i++ )
        {
            Projectile p = Instantiate<Projectile> ( projectile );
            p.gameObject.SetActive ( false );
            _projectilePool.Add ( p );
        }
    }
}
