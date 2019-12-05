using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenuPanel;
        private GameManager gameManager = GameManager.instance;

        public void TogglePause()
        {
            gameManager.gameIsPaused = !gameManager.gameIsPaused;
            pauseMenuPanel.SetActive(gameManager.gameIsPaused);
            Time.timeScale = 1 - Time.timeScale;
        }

        private void Update()
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
}