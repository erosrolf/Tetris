using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class TetrominoMovement : MonoBehaviour
{
    public void MoveLeft()
    {
        Debug.Log("left");
        Vector3 oldPosition = transform.position;
        transform.position += new Vector3(-1, 0, 0);
        if (IsInsideField() == false)
        {
            transform.position = oldPosition;
        }
    }

    public void MoveRight()
    {
        Debug.Log("right");
        Vector3 oldPosition = transform.position;
        transform.position += new Vector3(1, 0, 0);
        if (IsInsideField() == false)
        {
            transform.position = oldPosition;
        }
    }

    public void Rotate()
    {
        transform.eulerAngles += new Vector3(0, 0, 90);
    }

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
        Vector3 fieldPosition = FieldController.Position;
        float fieldWidth = FieldController.Width;
        float fieldHeight = FieldController.Height;

        foreach (Transform block in blocks)
        {
            if (block == transform) continue;
            Vector3 position = block.position;

            if (position.x < fieldPosition.x - fieldWidth / 2 ||
                position.x > fieldPosition.x + fieldWidth / 2 ||
                position.y < fieldPosition.y - fieldHeight / 2 ||
                position.y > fieldPosition.y + fieldHeight / 2)
            {
                return false;
            }
        }
        return true;
    }
}
