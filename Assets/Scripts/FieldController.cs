using UnityEngine;

public class FieldController : MonoBehaviour
{
    [SerializeField] private GameObject _placedBlocks;
    private static bool[,] _fieldMatrix;

    public static float Width { get; private set; }
    public static float Height { get; private set; }
    public static Vector3 Position { get; private set; }
    public static GameObject PlacedBlocks { get; private set; }

    public GameObject cubePrefab; // Префаб куба, установите его в редакторе Unity
    private GameObject visualizationParent; // Объект, который будет содержать визуализацию

    void Awake()
    {
        visualizationParent = new GameObject("MatrixVisualization");
        visualizationParent.transform.parent = transform;
        visualizationParent.transform.Translate(new Vector3(-11.5f, 9.5f, 0));

        Width = GetComponent<SpriteRenderer>().bounds.size.x;
        Height = GetComponent<SpriteRenderer>().bounds.size.y;
        Position = transform.position;
        _fieldMatrix = new bool[Mathf.RoundToInt(Width),
                                            Mathf.RoundToInt(Height)];
        PlacedBlocks = _placedBlocks;
        AutoFall.OnTetrominoFallen += DeleteFullRow;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            VisualizeMatrix(new Vector3(-15, -10, 0));
        }
    }

    private void VisualizeMatrix(Vector3 startPosition)
    {
        foreach (Transform child in visualizationParent.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < _fieldMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < _fieldMatrix.GetLength(1); j++)
            {
                if (_fieldMatrix[i, j])
                {
                    Vector3 position = new Vector3(startPosition.x + i, startPosition.y + j, startPosition.z);
                    Instantiate(cubePrefab, position, Quaternion.identity, visualizationParent.transform);
                }
            }
        }
    }

    void OnDestroy()
    {
        AutoFall.OnTetrominoFallen -= DeleteFullRow;
    }

    void LateUpdate()
    {
        if (IsAnyColFull())
        {
            GameState.GameOver();
        }
    }

    private void DeleteFullRow()
    {
        int rowForDelete = numOfFullRow();
        if (rowForDelete != -1)
        {
            Transform[] blocks = _placedBlocks.GetComponentsInChildren<Transform>();
            foreach (var block in blocks)
            {
                if (block == _placedBlocks.transform)
                {
                    continue;
                }
                int y = Mathf.FloorToInt(block.position.y + Height / 2);
                if (y == rowForDelete)
                {
                    Destroy(block.gameObject);
                }
            }
            LowerBlocksAfterRemoveRow(rowForDelete);
            DeleteAndShiftRowIntoMatrix(rowForDelete);
            GameState.AddScore(Mathf.FloorToInt(Width));
        }
    }

    private void DeleteAndShiftRowIntoMatrix(int deletedRow)
    {
        for (int i = 0; i < _fieldMatrix.GetLength(0); ++i)
        {
            for (int j = deletedRow; j < _fieldMatrix.GetLength(1) - 1; ++j)
            {
                _fieldMatrix[i, j] = _fieldMatrix[i, j + 1];
            }
        }
    }

    private void LowerBlocksAfterRemoveRow(int deletedRow)
    {
        Transform[] blocks = _placedBlocks.GetComponentsInChildren<Transform>();
        foreach (var block in blocks)
        {
            if (block == _placedBlocks.transform)
            {
                continue;
            }
            int y = Mathf.FloorToInt(block.position.y + Height / 2);
            if (y > deletedRow)
            {
                block.Translate(new Vector3(0, -1, 0));
            }
        }
    }

    public static void AddBlockToMatrix(Transform block)
    {
        int x = Mathf.FloorToInt(block.position.x + Width / 2);
        int y = Mathf.FloorToInt(block.position.y + Height / 2);
        _fieldMatrix[x, y] = true;
    }

    public static bool CheckCollision(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x + Width / 2);
        int y = Mathf.FloorToInt(position.y + Height / 2);
        if (x < 0 || x >= Width || y < 0 || y >= Height)
        {
            return true;
        }

        if (_fieldMatrix[x, y])
        {
            return true;
        }

        return false;
    }

    private int numOfFullRow()
    {
        for (int row = 0; row < Mathf.FloorToInt(Height); row++)
        {
            if (IsRowFull(row))
            {
                return row;
            }
        }
        return -1;
    }

    private bool IsRowFull(int row)
    {
        for (int col = 0; col < Width; col++)
        {
            if (!_fieldMatrix[col, row])
            {
                return false;
            }
        }
        return true;
    }

    private bool IsAnyColFull()
    {
        for (int col = 0; col < Width; col++)
        {
            if (IsColFull(col))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsColFull(int col)
    {
        for (int row = 0; row < Height; row++)
        {
            if (!_fieldMatrix[col, row])
            {
                return false;
            }
        }
        return true;
    }
}
