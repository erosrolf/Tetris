using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TetrominoSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tetrominoPrefabs;
    [SerializeField] private List<Color> _colors;
    [SerializeField] public PlayerController playerController;
    [SerializeField] private Transform nextFigurePosition;
    public GameObject PreparedTetromino { get; private set; }

    void Start()
    {
        SpawnTetromino();
    }

    void OnEnable()
    {
        if (PreparedTetromino != null)
        {
            PreparedTetromino.SetActive(true);
        }
        AutoFall.OnTetrominoFallen += SpawnTetromino;
    }

    void OnDisable()
    {
        if (PreparedTetromino != null)
        {
            PreparedTetromino.SetActive(false);
        }
        AutoFall.OnTetrominoFallen -= SpawnTetromino;
    }

    public void SpawnTetromino()
    {
        if (PreparedTetromino != null)
        {
            PreparedTetromino.transform.position = transform.position - PlacingOffset();
            if (PreparedTetromino.GetComponent<TetrominoMovement>().WillCollide(Vector3.zero))
            {
                AudioManager.Instance.PlaySFX("GameOver");
                GameManager.GameOver();
            }
            else
            {
                PreparedTetromino.GetComponent<AutoFall>().enabled = true;
                playerController.SetCurrentTetromino(PreparedTetromino.GetComponent<TetrominoMovement>());
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
        float highestBlockY = PreparedTetromino.GetComponentsInChildren<Transform>().Max(t => t.position.y);
        float offset = highestBlockY - PreparedTetromino.transform.position.y;
        return new Vector3(0, offset, 0);
    }

    private void PrepareTetromino()
    {
        int randomIndex = Random.Range(0, _tetrominoPrefabs.Count);
        PreparedTetromino = Instantiate(_tetrominoPrefabs[randomIndex], nextFigurePosition.position, Quaternion.identity);
        PreparedTetromino.GetComponent<AutoFall>().enabled = false;
        Color randomColor = _colors[Random.Range(0, _colors.Count)];
        SpriteRenderer[] spriteRenderers = PreparedTetromino.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = randomColor;
        }
    }
}
