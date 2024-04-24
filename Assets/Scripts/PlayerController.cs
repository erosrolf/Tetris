using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TetrominoMovement _currentTetromino;
    private float _oldSpeed;

    public void SetCurrentTetromino(TetrominoMovement newTetromino)
    {
        _currentTetromino = newTetromino;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentTetromino.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentTetromino.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentTetromino.Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _oldSpeed = GameSettings.instance.fallSpeed;
            GameSettings.instance.fallSpeed = 0.1f;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            GameSettings.instance.fallSpeed = _oldSpeed;
        }
    }
}