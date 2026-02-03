using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    private PlayerControls controls;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel = GameObject.Find("Game Over Panel");
        gameOverPanel.SetActive(false);
    }

    public void EnableGameOver()
    {
        gameOverPanel.SetActive(true);
        controls = new PlayerControls();
        controls.Player.Enable();
        controls.Player.Restart.performed += ctx => RestartGame();
    }

    public void RestartGame()
    {
        controls.Player.Disable();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
