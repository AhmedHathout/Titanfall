using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private static SceneChanger sceneChanger;

    [HideInInspector]
    public int currentLevel = 0;
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
        //pausePanel = GameObject.Find("Pause Panel");
        //pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        if (currentLevel == 1) {
            sceneChanger.Level1();
        }
        else {
            sceneChanger.Level2();
        }
    }
}
