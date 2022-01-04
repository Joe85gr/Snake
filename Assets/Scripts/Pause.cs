using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool GameIsPaused;

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) ResumeGame();
            else PauseGame();
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        GameIsPaused = true;
    }


}