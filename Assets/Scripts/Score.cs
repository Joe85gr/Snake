using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;

    private void Update()
    {
        if (Pause.GameIsPaused) return;
        var points = Snake.GetScore().ToString(CultureInfo.InvariantCulture);
        score.text = $"Score: {points}";
    }
}