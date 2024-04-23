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
        if (Time.time - _lastFallTime >= GameSettings.instance.fallSpeed)
        {
            Vector3 newPosition = transform.position + new Vector3(0, -1, 0);
            Vector3 oldPosition = transform.position;
            transform.position = newPosition;
            if (IsAtBottom())
            {
                transform.position = oldPosition;
                foreach (Transform block in GetComponentsInChildren<Transform>())
                {
                    if (block != transform)
                    {
                        FieldController.AddBlockToMatrix(block);
                        block.parent = FieldController.PlacedBlocks.transform;
                    }
                }
                OnTetrominoFallen?.Invoke();
                Destroy(this);
            }
            _lastFallTime = Time.time;
        }
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
