using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class AutoFall : MonoBehaviour
{
    private float _lastFallTime;
    public delegate void TetrominoFallenHandler();
    public static event TetrominoFallenHandler OnTetrominoFallen;

    void Update()
    {
        if (GameManager.GetCurrentState() != GameManager.State.Playing)
        {
            return;
        }

        float speed = 1.1f - (GameSettings.GetSpeed() / 10);
        if (Time.time - _lastFallTime >= speed)
        {
            Vector3 newPosition = transform.position + new Vector3(0, -1, 0);
            Vector3 oldPosition = transform.position;
            transform.position = newPosition;
            if (IsAtBottom())
            {
                transform.position = oldPosition;
                PlaceFigure();
            }
            _lastFallTime = Time.time;
        }
    }

    public void PlaceFigure()
    {
        foreach (Transform block in GetComponentsInChildren<Transform>())
        {
            if (block != transform)
            {
                FieldController.AddBlockToMatrix(block);
                block.parent = FieldController.PlacedBlocks.transform;
            }
        }
        OnTetrominoFallen?.Invoke();
        Destroy(gameObject);
    }

    private bool IsAtBottom()
    {
        Transform[] blocks = GetComponentsInChildren<Transform>();

        foreach (Transform block in blocks)
        {
            if (block == transform) continue;

            Vector3 position = block.position;

            if (position.y <= FieldController.Position.y - FieldController.Height / 2 ||
                OnPlacedBlocks(position))
            {
                return true;
            }
        }
        return false;
    }

    private bool OnPlacedBlocks(Vector3 blockPosition)
    {
        Transform[] placedBlocks = FieldController.PlacedBlocks.GetComponentsInChildren<Transform>();
        foreach (Transform placedBlock in placedBlocks)
        {
            if (placedBlock.position == blockPosition)
            {
                return true;
            }
        }
        return false;
    }
}
