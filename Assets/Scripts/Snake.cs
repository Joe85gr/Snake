using System.Collections.Generic;
using Domain;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public PlayerName playerName;
    private Vector2 _direction = Vector2.right;
    private static List<Transform> _snakeBody;
    private const int StartLenght = 4;
    private static bool _gameOver;

    public Transform segmentPrefab;

    public static float GetScore()
    {
        var score = (_snakeBody.Count - 5) * 3 * (Time.timeScale * 10);

        if (_gameOver == false) Time.timeScale = Speed.Get(score);

        return score;
    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        ResetSnake();
        InitialiseSnake();
    }

    private void Start()
    {
        _snakeBody = new List<Transform> {transform};
        //var scores = SaveSystem.Load();
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
        MoveSnake();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food")) Grow();
        else if (other.CompareTag("Walls"))
        {
            Time.timeScale = 0f;
            _gameOver = true;
            var score = GetScore();
            playerName.Show(score);
        }
    }

    private void InitialiseSnake()
    {
        _direction = Vector2.right;

        for (var i = 0; i < StartLenght; i++)
        {
            Grow();
        }
    }

    private void MoveSnake()
    {
        for (var i = _snakeBody.Count - 1; i > 0; i--)
        {
            _snakeBody[i].position = _snakeBody[i - 1].position;
        }

        var tempTransform = transform;
        var position = tempTransform.position;

        position = new Vector3(
            position.x + (_direction.x / 2),
            position.y + (_direction.y / 2),
            0.0f
        );

        tempTransform.position = position;
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
        _gameOver = false;
    }
}