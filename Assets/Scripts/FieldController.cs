using System;
using Unity.VisualScripting;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    [SerializeField] private GameObject _placedBlocks;
    private static bool[,] _fieldMatrix;

    public static float Width { get; private set; }
    public static float Height { get; private set; }
    public static Vector3 Position { get; private set; }
    public static GameObject PlacedBlocks { get; private set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PrintFieldMatrix(0);
        }
    }

    void LateUpdate()
    {
        if (IsAnyColFull())
        {
            GameState.GameOver();
        }
    }

    public static void PrintFieldMatrix(int row)
    {
        string line = "\n";
        for (int x = 0; x < Width; x++)
        {
            line += _fieldMatrix[x, row] ? "1" : "0";
            line += " ";
        }
        Debug.Log(line);
    }


    void Awake()
    {
        Width = GetComponent<SpriteRenderer>().bounds.size.x;
        Height = GetComponent<SpriteRenderer>().bounds.size.y;
        Position = transform.position;
        _fieldMatrix = new bool[Mathf.RoundToInt(Width),
                                            Mathf.RoundToInt(Height)];
        PlacedBlocks = _placedBlocks;
    }

    public static void AddBlockToMatrix(Transform block)
    {
        int x = Mathf.FloorToInt(block.position.x + Width / 2);
        int y = Mathf.FloorToInt(block.position.y + Height / 2);
        _fieldMatrix[x, y] = true;
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
