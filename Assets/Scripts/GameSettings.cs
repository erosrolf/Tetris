using UnityEngine;

public class GameSettings : MonoBehaviour
{
    // Скорость падения тетромино
    [SerializeField] public float fallSpeed = 0;
    [SerializeField] public GameObject field = null;

    // Сделаем этот класс синглтоном, чтобы мы могли легко получить доступ к нему из других скриптов
    public static GameSettings instance;

    void Awake()
    {
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
}
