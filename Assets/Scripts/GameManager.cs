using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static SceneChanger sceneChanger;

    [HideInInspector]
    public int currentLevel = 0;
    [HideInInspector]
    public bool gameIsPaused = false;

    private GameObject pausePanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
    }

    private void Update()
    {
        // Next Level Cheats
        LoadNextLevel();
    }

    public void StartGame()
    {
        SetCurrentLevel(1);
        sceneChanger.Level1();
    }
    // Update is called once per frame


    //public void TogglePause()
    //{
    //    gameIsPaused = !gameIsPaused;
    //    pausePanel.SetActive(gameIsPaused);
    //    Time.timeScale = 1 - Time.timeScale;
    //}

    public void SetCurrentLevel(int newLevel)
    {
        this.currentLevel = newLevel;

        if (this.currentLevel == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Screen.lockCursor = false;
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Level Restarted");
        if (currentLevel == 1) {
            sceneChanger.Level1();
        }
        else if (currentLevel == 2) {
            sceneChanger.Level2();
        }

        else
        {
            Debug.LogWarning("Cannot restart level: " + currentLevel);
        }
    }

    public void LoadGameOver()
    {
        SetCurrentLevel(0);
        sceneChanger.GameOver();
    }

    public void LoadCredits()
    {
        SetCurrentLevel(0);
        sceneChanger.Credits();
    }

    public void LoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            
            if (currentLevel == 0)
            {
                SetCurrentLevel(1);
                sceneChanger.Level1();
            }

            else if (currentLevel == 1)
            {
                SetCurrentLevel(2);
                sceneChanger.Level2();
            }

            else if (currentLevel == 2)
            {
                SetCurrentLevel(0);
                sceneChanger.Credits();
                GameObject FPS = GameObject.Find("FPSController");
                if (FPS != null)
                {
                    Destroy(FPS);
                }
            }
        }
    }

    public void LoadMainMenu()
    {
        SetCurrentLevel(0);
        GameObject FPS = GameObject.Find("FPSController");
        if (FPS != null)
        {
            Destroy(FPS);
        }
        sceneChanger.MainMenu();
    }

}
