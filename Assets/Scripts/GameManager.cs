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

    // Update is called once per frame


    //public void TogglePause()
    //{
    //    gameIsPaused = !gameIsPaused;
    //    pausePanel.SetActive(gameIsPaused);
    //    Time.timeScale = 1 - Time.timeScale;
    //}

    public void setCurrentLevel(int newLevel)
    {
        this.currentLevel = newLevel;

        if (this.currentLevel == 0)
        {
            Debug.Log("Cursor should be enabled");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //Screen.lockCursor = false;
        }
        //pausePanel = GameObject.Find("Pause Panel");
        //pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
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

    public void LoadGameFinished()
    {
        setCurrentLevel(0);
        sceneChanger.GameFinished();
    }

    public void LoadGameOver()
    {
        setCurrentLevel(0);
        sceneChanger.GameOver();
    }

    public void LoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentLevel == 0)
            {
                setCurrentLevel(1);
                sceneChanger.Level1();
            }

            else if (currentLevel == 1)
            {
                setCurrentLevel(2);
                sceneChanger.Level2();
            }

            else if (currentLevel == 2)
            {
                setCurrentLevel(0);
                sceneChanger.GameFinished();
            }
        }
    }
}
