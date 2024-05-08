using TMPro;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float _fallSpeed;
    [SerializeField] public GameObject field = null;
    [SerializeField] public TextMeshProUGUI speedLabel;
    [SerializeField] public bool autoAcceleration;
    [SerializeField] public int scoreForAcceleration;
    [SerializeField] private int _maxSpeed;

    // Сделаем этот класс синглтоном, чтобы мы могли легко получить доступ к нему из других скриптов
    public static GameSettings instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static float GetSpeed()
    {
        return instance._fallSpeed;
    }

    public void SpeedIncrease()
    {
        Debug.Log("speed = " + _fallSpeed);
        if (_fallSpeed < _maxSpeed)
        {
            Debug.Log("speed++");
            ++_fallSpeed;
            speedLabel.text = _fallSpeed.ToString();
        }
    }

    public void SpeedDecrease()
    {
        if (_fallSpeed > 1)
        {
            --_fallSpeed;
            speedLabel.text = _fallSpeed.ToString();
        }
    }
    public void SetSpeed(int speed)
    {
        if (speed >= 1 && speed <= _maxSpeed)
        {
            _fallSpeed = speed;
        }
    }
}

