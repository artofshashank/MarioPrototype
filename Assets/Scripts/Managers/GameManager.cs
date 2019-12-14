using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameManager class.
/// At this point only handles Player and Display Manager.
/// Later It can be extended to dynamically spawn player and enemies, have more rules in the game, etc.
/// </summary>
public class GameManager : MonoBehaviour
{
    public PlayerController _player;
    public DisplayManager _displayManager;

    private void Awake ( )
    {
        _displayManager.RestartPressed += RestartLevel;
        _player.PlayerWin += DisplayPlayerResult;
        _player.ScoreIncreased +=   DisplayScore;
    }

    private void DisplayPlayerResult ( bool win)
    {
        _displayManager.DisplayResult ( win );
    }

    private void DisplayScore ( int score )
    {
        _displayManager.UpdateScore ( score );
    }

    private void RestartLevel ( )
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene ( 0 );
    }
}
