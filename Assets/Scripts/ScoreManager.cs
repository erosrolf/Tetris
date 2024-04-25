using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public void AddScore(int amount)
    {
        _score += amount;
        _scoreText.text = _score.ToString();
    }
}
