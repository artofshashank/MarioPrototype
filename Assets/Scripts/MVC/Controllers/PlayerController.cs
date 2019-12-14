using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

/// <summary>
/// MVC Controller for Player.
/// Movement, jump and shoot through input control handled here.
/// Score also handled here.
/// Player win/lose sent to GameManager to handle.
/// </summary>
public class PlayerController : CharacterController
{
    public PlayerInputControl _inputControl;
    public Action<bool> PlayerWin;
    public Action<int> ScoreIncreased;

    private PlayerView _view;
    private PlayerModel _model;

    private bool _movingRight;
    private bool _movingLeft;
    private bool _isJumping;

    private void Awake ( )
    {
        _view = GetComponent<PlayerView> ( );
        _model = GetComponent<PlayerModel> ( );

        _model.InitializeProjectilePool ( _view._projectilePrefab, 20 );

        SetBaseModelView ( _model, _view );
    }

    private void Start ( )
    {
        _inputControl.LeftButton += ( isPressed ) => { _movingLeft = isPressed; } ;
        _inputControl.RightButton += ( isPressed ) => { _movingRight = isPressed; };
        _inputControl.Jump += Jump;
        _inputControl.Shoot += Shoot;

        _view.CharacterKilled +=  PlayerDeath;
        _view.CastleTouched +=  () => PlayerWin.Invoke(true) ;
        _view.CoinCollected += IncrementScore ;
    }

    private void FixedUpdate ( )
    {
        if ( _movingRight )
        {
            MoveInDirection ( CharacterMoveDirection.Right );
        }
        else if ( _movingLeft )
        {
            MoveInDirection ( CharacterMoveDirection.Left );
        }
    }

    private void Shoot ( )
    {
        Projectile prjctl = _model.GetNextValidProjectile ( );
        if ( prjctl == null )
            return;

        prjctl.gameObject.SetActive ( true );
        prjctl.transform.position = _view._shooter.position;
        prjctl._rigidBody.velocity = _view.transform.forward * _model._projectileSpeed;
    }

    private void Jump ( )
    {
        if ( _view.IsGrounded ( ) )
        {
            _view._rigidBody.AddForce ( Vector3.up * _model._jumpForceMultiplier, ForceMode.Impulse );
        }
    }

    private void IncrementScore ( )
    {
        _model.score += 1;
        ScoreIncreased.Invoke ( _model.score );
        Debug.Log ( _model.score);
    }

    private void PlayerDeath ( )
    {
        PlayerWin.Invoke ( false );
        Dead ( );
    }
}