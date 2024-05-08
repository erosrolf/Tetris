using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menuObjects;
    [SerializeField] private List<GameObject> _playingObjects;
    [SerializeField] private List<GameObject> _gameOverObjects;

    public static Action OnStartMenuStage;
    public static Action OnStartPlayingStage;
    public static Action PauseOn;
    public static Action PauseOff;
    public static Action OnStartGameOverStage;

    public enum State { Menu, Playing, GameOver, Paused }
    private static State _currentState;

    private static GameManager _instance;

    void Awake()
    {
        _currentState = State.Menu;
        _instance = this;
        OnStartMenuStage?.Invoke();
    }

    public static void GameOver()
    {
        if (_currentState == State.Playing)
        {
            _currentState = State.GameOver;
            _instance.SetActiveGroup(_instance._playingObjects, false);
            _instance.SetActiveGroup(_instance._gameOverObjects, true);
            OnStartGameOverStage?.Invoke();
            AudioManager.Instance.PlaySFX("GameOver");
        }
    }

    public static void Play()
    {
        if (_currentState == State.Menu || _currentState == State.Paused)
        {
            _instance.SetActiveGroup(_instance._menuObjects, false);
            _instance.SetActiveGroup(_instance._gameOverObjects, false);
            _instance.SetActiveGroup(_instance._playingObjects, true);
            if (_currentState == State.Menu)
            {
                AudioManager.Instance.PlayMusic("GameMusic");
                OnStartPlayingStage?.Invoke();
            }
            else
            {
                AudioManager.Instance.UnPauseMusic();
                PauseOff?.Invoke();
            }
            _currentState = State.Playing;
        }
    }

    public static void NewGame()
    {
        if (_currentState == State.GameOver)
        {
            GameSettings.instance.SetSpeed(1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Play();
        }
    }

    public static void Pause()
    {
        if (_currentState == State.Playing)
        {
            _currentState = State.Paused;
            _instance.SetActiveGroup(_instance._playingObjects, false);
            _instance.SetActiveGroup(_instance._menuObjects, true);
            AudioManager.Instance.PauseMusic();
            PauseOn?.Invoke();
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
