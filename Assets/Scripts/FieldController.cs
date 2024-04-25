using UnityEngine;

public class FieldController : MonoBehaviour
{
    [SerializeField] private GameObject _placedBlocks;
    private static bool[,] _fieldMatrix;

    public static float Width { get; private set; }
    public static float Height { get; private set; }
    public static Vector3 Position { get; private set; }
    public static GameObject PlacedBlocks { get; private set; }

    public static bool[,] GetMatrix() => _fieldMatrix;

    void Awake()
    {
        Width = GetComponent<SpriteRenderer>().bounds.size.x;
        Height = GetComponent<SpriteRenderer>().bounds.size.y;
        Position = transform.position;
        _fieldMatrix = new bool[Mathf.RoundToInt(Width),
                                            Mathf.RoundToInt(Height)];
        PlacedBlocks = _placedBlocks;
        AutoFall.OnTetrominoFallen += DeleteFullRow;
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
        while (numOfFullRow(out int rowForDelete) != -1)
        {
            DeleteBlocksOnFullLine(rowForDelete);
            ShiftBlocksAfterRemoveRow(rowForDelete);
            DeleteAndShiftRowIntoMatrix(rowForDelete);
            GameState.AddScore(Mathf.FloorToInt(Width));
        }
    }

    private void DeleteBlocksOnFullLine(int deletedRow)
    {
        Transform[] blocks = _placedBlocks.GetComponentsInChildren<Transform>();
        foreach (var block in blocks)
        {
            if (block == _placedBlocks.transform)
            {
                continue;
            }
            int y = Mathf.FloorToInt(block.position.y + Height / 2);
            if (y == deletedRow)
            {
                Destroy(block.gameObject);
            }
        }
    }

    private void ShiftBlocksAfterRemoveRow(int deletedRow)
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

        if (x < 0 || x >= Width || y < 0 || y >= Height) return true;
        if (_fieldMatrix[x, y]) return true;
        return false;
    }

    private int numOfFullRow(out int row)
    {
        for (row = _fieldMatrix.GetLength(0) - 1; row >= 0; row--)
            if (IsRowFull(row)) return row;
        return -1;
    }

    private bool IsRowFull(int row)
    {
        for (int col = 0; col < Width; col++)
            if (!_fieldMatrix[col, row]) return false;
        return true;
    }

    private bool IsAnyColFull()
    {
        for (int col = 0; col < Width; col++)
            if (IsColFull(col)) return true;
        return false;
    }

    private bool IsColFull(int col)
    {
        for (int row = 0; row < Height; row++)
            if (!_fieldMatrix[col, row]) return false;
        return true;
    }
}
