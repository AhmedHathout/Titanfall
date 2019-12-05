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
            gameManager.TogglePause(pauseMenuPanel);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameManager.TogglePause(pauseMenuPanel);
            }
        }
    }
}