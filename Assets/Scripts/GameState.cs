using UnityEngine;

public class GameState : MonoBehaviour
{
    private static int _score = 0;
    public enum State { Playing, GameOver, Paused }
    private static State _currentState = State.Playing;

    public static void AddScore(int score)
    {
        _score += score;
    }

    public static void GameOver()
    {
        if (_currentState == State.Playing)
        {
            _currentState = State.GameOver;
        }
    }

    public static void Pause()
    {
        if (_currentState == State.Playing)
        {
            _currentState = State.Paused;
        }
        else if (_currentState == State.Paused)
        {
            _currentState = State.Playing;
        }
    }

    public static State GetCurrentState()
    {
        return _currentState;
    }
}
