using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;


public class TetrominoSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tetrominoPrefabs;
    [SerializeField] private List<Color> _colors;
    [SerializeField] public PlayerController playerController;
    [SerializeField] private Transform nextFigurePosition;
    private GameObject preparedTetromino;

    void Start()
    {
        SpawnTetromino();
    }

    void OnEnable()
    {
        AutoFall.OnTetrominoFallen += SpawnTetromino;
    }

    void OnDisable()
    {
        AutoFall.OnTetrominoFallen -= SpawnTetromino;
    }

    public void SpawnTetromino()
    {
        if (preparedTetromino != null)
        {
            preparedTetromino.transform.position = transform.position - PlacingOffset();
            if (preparedTetromino.GetComponent<TetrominoMovement>().WillCollide(Vector3.zero))
            {
                GameManager.GameOver();
            }
            else
            {
                preparedTetromino.GetComponent<AutoFall>().enabled = true;
                playerController.SetCurrentTetromino(preparedTetromino.GetComponent<TetrominoMovement>());
                PrepareTetromino();
            }
        }
        else
        {
            PrepareTetromino();
            SpawnTetromino();
        }
    }

    private Vector3 PlacingOffset()
    {
        float highestBlockY = preparedTetromino.GetComponentsInChildren<Transform>().Max(t => t.position.y);
        float offset = highestBlockY - preparedTetromino.transform.position.y;
        return new Vector3(0, offset, 0);
    }

    private void PrepareTetromino()
    {
        int randomIndex = Random.Range(0, _tetrominoPrefabs.Count);
        preparedTetromino = Instantiate(_tetrominoPrefabs[randomIndex], nextFigurePosition.position, Quaternion.identity);
        preparedTetromino.GetComponent<AutoFall>().enabled = false;
        Color randomColor = _colors[Random.Range(0, _colors.Count)];
        SpriteRenderer[] spriteRenderers = preparedTetromino.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = randomColor;
        }
    }
}
