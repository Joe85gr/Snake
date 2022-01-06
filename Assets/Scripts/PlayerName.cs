using Services;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    private string _player;
    private float _score;
    private Pause _pause;
    
    public GameObject playerName;
    public Snake snake;
    public Pause canvas;

    public void StoreName()
    {
        _player = playerName.GetComponent<Text>().text;
        SaveService.Save(_player, _score);
        transform.gameObject.SetActive(false);
        snake.ResetGame();
        _pause.enabled = true;
        Time.timeScale = 1f;
    }

    public void Show(float score)
    {
        _pause = canvas.GetComponent<Pause>();
        _pause.enabled = false;
        _score = score;
        playerName.GetComponent<Text>().text = "";
        transform.gameObject.SetActive(true);
    }
}
