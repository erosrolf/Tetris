using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TetrominoSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tetrominoPrefabs;
    [SerializeField] private List<Color> _colors;

    void Start()
    {
        SpawnTetromino();
    }

    void OnEnable()
    {
        // Подписываемся на событие
        AutoFall.OnTetrominoFallen += SpawnTetromino;
    }

    void OnDisable()
    {
        // Отписываемся от события
        AutoFall.OnTetrominoFallen -= SpawnTetromino;
    }

    public void SpawnTetromino()
    {
        int randomIndex = Random.Range(0, _tetrominoPrefabs.Count);
        GameObject newTetromino = Instantiate(_tetrominoPrefabs[randomIndex], transform.position, Quaternion.identity);
        Color randomColor = _colors[Random.Range(0, _colors.Count)];

        SpriteRenderer[] spriteRenderers = newTetromino.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = randomColor;
        }
    }

}
