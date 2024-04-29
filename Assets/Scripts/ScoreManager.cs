using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;

    void Start()
    {
        Debug.Log("into score manager start " + GameSettings.GetSpeed());
    }

    public void AddScore(int amount)
    {
        if (GameSettings.instance.autoAcceleration)
        {
            if (_score / GameSettings.instance.scoreForAcceleration <
                GameSettings.GetSpeed() && _score < 1000)
            {
                Debug.Log("into score manager fallspeed" + GameSettings.GetSpeed());
                GameSettings.instance.SpeedIncrease();
            }
            else if (_score > 1000)
            {
                Debug.Log("into score manager fallspeed" + GameSettings.GetSpeed());
                GameSettings.instance.SpeedIncrease();
            }
        }
        _score += amount;
        _gameOverScoreText.text = _scoreText.text = _score.ToString();
    }
}
