using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    private GameManager gameManager;
    public void TogglePause()
    {
        gameManager.gameIsPaused = !gameManager.gameIsPaused;
        pauseMenuPanel.SetActive(gameManager.gameIsPaused);
        Time.timeScale = 1 - Time.timeScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.currentLevel > 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }
}
