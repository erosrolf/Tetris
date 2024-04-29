using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menuObjects;
    [SerializeField] private List<GameObject> _playingObjects;
    [SerializeField] private List<GameObject> _gameOverObjects;

    public enum State { Menu, Playing, GameOver, Paused }
    private static State _currentState;

    private static GameManager _instance;

    void Awake()
    {
        _currentState = State.Menu;
        _instance = this;
    }

    public static void GameOver()
    {
        if (_currentState == State.Playing)
        {
            _currentState = State.GameOver;
            _instance.SetActiveGroup(_instance._playingObjects, false);
            _instance.SetActiveGroup(_instance._gameOverObjects, true);
        }
    }

    public static void Play()
    {
        if (_currentState == State.Menu || _currentState == State.Paused)
        {
            _currentState = State.Playing;
            _instance.SetActiveGroup(_instance._menuObjects, false);
            _instance.SetActiveGroup(_instance._gameOverObjects, false);
            _instance.SetActiveGroup(_instance._playingObjects, true);
        }
    }

    public static void NewGame()
    {
        if (_currentState == State.GameOver)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // _currentState = State.Playing;
            // _instance.SetActiveGroup(_instance._menuObjects, false);
            // _instance.SetActiveGroup(_instance._gameOverObjects, false);
            // _instance.SetActiveGroup(_instance._gameOverObjects, true);
        }
    }

    public static void Pause()
    {
        if (_currentState == State.Playing)
        {
            _currentState = State.Paused;
            _instance.SetActiveGroup(_instance._playingObjects, false);
            _instance.SetActiveGroup(_instance._menuObjects, true);
        }
    }

    public static State GetCurrentState()
    {
        return _currentState;
    }

    private void SetActiveGroup(List<GameObject> group, bool active)
    {
        foreach (var gameObject in group)
        {
            gameObject.SetActive(active);
        }
    }
}
