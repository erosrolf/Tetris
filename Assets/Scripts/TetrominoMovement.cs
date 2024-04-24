using UnityEngine;

public class TetrominoMovement : MonoBehaviour
{
    [SerializeField] private bool canRotate;

    public void MoveLeft()
    {
        Vector3 movement = new Vector3(-1, 0, 0);
        if (WillCollide(movement) == false)
        {
            transform.position += movement;
        }
    }

    public void MoveRight()
    {
        Vector3 movement = new Vector3(1, 0, 0);
        if (WillCollide(movement) == false)
        {
            transform.position += movement;
        }
    }

    public void Rotate()
    {
        if (canRotate)
        {
            Vector3 oldRotation = transform.eulerAngles;

            if (transform.eulerAngles == Vector3.zero)
            {
                transform.eulerAngles += new Vector3(0, 0, 90);
            }
            else
            {
                transform.eulerAngles += new Vector3(0, 0, -90);
            }

            if (WillCollide(Vector3.zero))
            {
                transform.eulerAngles = oldRotation;
            }
        }
    }

    private bool WillCollide(Vector3 movement)
    {
        Transform[] blocks = GetComponentsInChildren<Transform>();
        foreach (var block in blocks)
        {
            Vector3 newPosition = block.position + movement;
            if (FieldController.CheckCollision(newPosition))
            {
                return true;
            }
        }
        return false;
    }
}
