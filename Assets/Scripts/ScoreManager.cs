using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    void Start()
    {
        Debug.Log("into score manager start " + GameSettings.GetSpeed());
    }

    public void AddScore(int amount)
    {
        if (GameSettings.instance.autoAcceleration)
        {
            if (_score / GameSettings.instance.scoreForAcceleration <
                GameSettings.GetSpeed())
            {
                Debug.Log("into score manager fallspeed" + GameSettings.GetSpeed());
                GameSettings.instance.SpeedIncrease();
            }
        }
        _score += amount;
        _scoreText.text = _score.ToString();
    }
}
