using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private static List<Transform> _snakeBody;
    private const int StartLenght = 4;

    public Transform segmentPrefab;

    public static float GetScore()
    {
        var score = (_snakeBody.Count - 5) * 3 * (Time.timeScale * 10);

        if (score > (float) Level.Level1 && score < (float) Level.Level2) Time.timeScale = 1.1f;
        else if (score > (float) Level.Level2 && score < (float) Level.Level3) Time.timeScale = 1.2f;
        else if (score > (float) Level.Level3 && score < (float) Level.Level4) Time.timeScale = 1.3f;
        else if (score > (float) Level.Level4 && score < (float) Level.Level5) Time.timeScale = 1.4f;
        else if (score > (float) Level.Level5) Time.timeScale = 1.6f;

        return score;
    }

    private void Start()
    {
        _snakeBody = new List<Transform> {transform};
        InitialiseSnake();
    }

    private void Update()
    {
        if (Pause.GameIsPaused) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down)
            _direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up)
            _direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right)
            _direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left)
            _direction = Vector2.right;
    }

    private void FixedUpdate()
    {
        for (var i = _snakeBody.Count - 1; i > 0; i--)
        {
            _snakeBody[i].position = _snakeBody[i - 1].position;
        }

        var transform1 = transform;
        var position = transform1.position;

        position = new Vector3(
            position.x + (_direction.x / 2),
            position.y + (_direction.y / 2),
            0.0f
        );

        transform1.position = position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food")) Grow();
        else if (other.CompareTag("Walls")) ResetSnake();
    }

    private void InitialiseSnake()
    {
        _direction = Vector2.right;

        for (var i = 0; i < StartLenght; i++)
        {
            Grow();
        }
    }

    private void Grow()
    {
        var segment = Instantiate(segmentPrefab);
        segment.position = _snakeBody[_snakeBody.Count - 1].position;
        _snakeBody.Add(segment);
    }

    private void ResetSnake()
    {
        for (var i = 1; i < _snakeBody.Count; i++)
        {
            Destroy(_snakeBody[i].gameObject);
        }

        _snakeBody.Clear();

        _snakeBody.Add(transform);

        transform.position = Vector3.zero;

        Time.timeScale = 1f;

        InitialiseSnake();
    }
}