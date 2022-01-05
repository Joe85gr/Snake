using Services;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    private string _player;
    private float _score;
    public GameObject playerName;
    public Snake snake;

    public void StoreName()
    {
        _player = playerName.GetComponent<Text>().text;
        SaveService.Save(_player, _score);
        transform.gameObject.SetActive(false);
        snake.ResetGame();
    }

    public void Show(float score)
    {
        _score = score;
        playerName.GetComponent<Text>().text = "";
        transform.gameObject.SetActive(true);
    }
}
