using JetBrains.Annotations;
using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    private bool CheckCollision()
    {
        CompositeCollider2D compositeCollider = GetComponent<CompositeCollider2D>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

        Collider2D hitCollider = Physics2D.OverlapBox(rigidbody2D.position, compositeCollider.bounds.size, 0);

        if (hitCollider != null && hitCollider.transform != transform)
        {
            return true;
        }

        return false;
    }

    private bool IsInsideField()
    {
        Transform[] blocks = GetComponentsInChildren<Transform>();

        foreach (Transform block in blocks)
        {
            // Если этот Transform - это сама фигура, пропускаем его
            if (block == transform) continue;

            Vector3 position = block.position;

            // Проверяем, находится ли блок внутри границ игрового поля
            if (position.x < FieldController.Position.x ||
                position.x > FieldController.Position.x + FieldController.Width ||
                position.y < FieldController.Position.y ||
                position.y > FieldController.Position.y + FieldController.Height)
            {
                return false;
            }
        }

        return true;
    }
}