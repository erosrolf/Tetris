using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private TetrominoMovement _currentTetromino;
    private float _timer;
    private float _delay = 0.05f;

    public void SetCurrentTetromino(TetrominoMovement newTetromino)
    {
        _currentTetromino = newTetromino;
    }

    void Update()
    {
        _timer += Time.deltaTime;

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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentTetromino.FallDown();
        }
        else if (Input.GetKey(KeyCode.DownArrow) && _timer > _delay)
        {
            _currentTetromino.MoveDown();
            _timer = 0;
        }
    }
}
