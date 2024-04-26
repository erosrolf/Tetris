using TMPro;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private float _fallSpeed = 0;
    [SerializeField] public GameObject field = null;
    [SerializeField] public TextMeshProUGUI speedLabel;
    [SerializeField] public bool autoAcceleration;
    [SerializeField] public int scoreForAcceleration;
    [SerializeField] private int _maxSpeed;

    // Сделаем этот класс синглтоном, чтобы мы могли легко получить доступ к нему из других скриптов
    public static GameSettings instance;

    void Awake()
    {
        speedLabel.text = _fallSpeed.ToString();
        // Если уже есть другой экземпляр этого класса, уничтожим этот объект
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Если это первый экземпляр этого класса, сохраняем ссылку на него
        instance = this;

        // Убеждаемся, что этот объект не будет уничтожен при загрузке новой сцены
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
}

