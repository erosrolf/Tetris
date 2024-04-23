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
            PrintFieldMatrix();
        }
    }

    public static void PrintFieldMatrix()
    {
        string line = "\n";
        for (int x = 0; x < Width; x++)
        {
            // Добавляем к строке 1, если в этой ячейке есть блок, и 0 в противном случае
            line += _fieldMatrix[x, 0] ? "1" : "0";
            line += " ";
        }
        // Выводим строку в консоль
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

    bool IsLineFull(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            if (!_fieldMatrix[x, y])
            {
                return false;
            }
        }
        return true;
    }
}
