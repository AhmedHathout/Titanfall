using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private static SceneChanger sceneChanger;
    private string currentScene;

    [HideInInspector]
    public int currentLevel = 0;
    [HideInInspector]
    public bool gameIsPaused = false;

    private static string mainMenu = "MainMenu";
    private static string level1 = "Level1";
    private static string level2 = "Level2";
    private static string gameOver = "GameOver";
    private static string credits = "Credits";
    private static string howToPlay = "HowToPlay";

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

    void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == mainMenu)
        {
            //StartCoroutine(AssignMainMenuButtons());
            AssignMainMenuButtons();
        }
    }

    private void Update()
    {
        // Next Level Cheats
        LoadNextLevel();
    }

    private void AssignMainMenuButtons()
    {
        StartCoroutine(AssignStartGameButton());
        StartCoroutine(AssignHowToPlayButton());
        StartCoroutine(AssignCreditsButton());
    }

    public void StartGame()
    {
        currentScene = level1;
        sceneChanger.Level1();
        currentLevel = 1;
    }

    public void LoadHowToPlay()
    {
        currentScene = howToPlay;
        sceneChanger.HowToPlay();
        currentLevel = 0;
    }

    public void TogglePause(GameObject pauseMenuPanel)
    {
        gameIsPaused = !gameIsPaused;
        pauseMenuPanel.SetActive(gameIsPaused);
        Time.timeScale = 1 - Time.timeScale;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Screen.lockCursor = false;
        GameObject FPS = GameObject.Find("FPSController");
        if (FPS != null)
        {
            FPS.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    public void RestartLevel()
    {
        if (currentLevel == 1)
        {
            sceneChanger.Level1();
            //ToggleTimeScale();
        }
        else if (currentLevel == 2)
        {
            sceneChanger.Level2();
            //ToggleTimeScale();
        }
        else
        {
            Debug.LogError("Cannot restart " + currentScene);
        }
    }

    public void LoadGameOver()
    {
        currentScene = gameOver;
        sceneChanger.GameOver();
        ShowCursor();
        StartCoroutine(AssignRestartButton());
        StartCoroutine(AssignMainMenuButton());
    }

    public void LoadCredits()
    {
        currentScene = credits;
        sceneChanger.Credits();
        ShowCursor();
        StartCoroutine(AssignMainMenuButton());
    }

    private void LoadLevel1()
    {
        currentScene = level1;
        sceneChanger.Level1();
    }

    private void LoadLevel2()
    {
        currentScene = level2;
        sceneChanger.Level2();
    }

    public void LoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (currentScene == level1)
            {
                currentLevel = 2;
                LoadLevel2();
            }
            else if (currentScene == level2)
            {
                currentLevel = 0;
                LoadCredits();
            }
        }
    }

    public void LoadMainMenu()
    {
        currentScene = mainMenu;
        sceneChanger.MainMenu();
        ShowCursor();
        AssignMainMenuButtons();
    }

    private IEnumerator AssignMainMenuButton()
    {
        yield return new WaitForSeconds(0.05f);
        Button[] buttons = FindObjectOfType<Canvas>().GetComponentsInChildren<Button>(true);

        Button mainMenuButton = null;

        foreach (Button button in buttons)
        {
            if (button.name.Contains("Main"))
            {
                mainMenuButton = button;
            }
        }

        mainMenuButton.onClick.AddListener(delegate
        {
            LoadMainMenu();
        });
    }
        private IEnumerator AssignStartGameButton()
    {
        yield return new WaitForSeconds(0.05f);
        Button[] buttons = FindObjectOfType<Canvas>().GetComponentsInChildren<Button>(true);

        Button startGameButton = null;

        foreach (Button button in buttons)
        {
            if (button.name.Contains("Start"))
            {
                startGameButton = button;
            }
        }

        startGameButton.onClick.AddListener(delegate
        {
            StartGame();
        });
    }


    private IEnumerator AssignCreditsButton()
    {
        yield return new WaitForSeconds(0.05f);
        Button[] buttons = FindObjectOfType<Canvas>().GetComponentsInChildren<Button>(true);

        Button creditsButton = null;

        foreach (Button button in buttons)
        {
            if (button.name.Contains("Credits"))
            {
                creditsButton = button;
            }
        }

        creditsButton.onClick.AddListener(delegate
        {
            LoadCredits();
        });
    }
    private IEnumerator AssignHowToPlayButton()
    {
        yield return new WaitForSeconds(0.05f);
        Button[] buttons = FindObjectOfType<Canvas>().GetComponentsInChildren<Button>(true);

        Button howToPlayButton = null;

        foreach (Button button in buttons)
        {
            if (button.name.Contains("How"))
            {
                howToPlayButton = button;
            }
        }

        howToPlayButton.onClick.AddListener(delegate
        {
            LoadHowToPlay();
        });
    }
    private IEnumerator AssignRestartButton()
    {
        yield return new WaitForSeconds(0.05f);
        Button[] buttons = FindObjectOfType<Canvas>().GetComponentsInChildren<Button>(true);

        Button restartButton = null;

        foreach (Button button in buttons)
        {
            if (button.name.Contains("Restart"))
            {
                restartButton = button;
            }
        }

        restartButton.onClick.AddListener(delegate
        {
            RestartLevel();
        });
    }

    public void ToggleTimeScale()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = 1 - Time.timeScale;
    }
    //private void DestroyFPSController()
    //{
    //    ShowCursor();
    //    GameObject FPS = GameObject.Find("FPSController");
    //    Debug.Log(FPS);
    //    if (FPS != null)
    //    {
    //        Debug.Log("Destroying FPS");
    //        Destroy(FPS);
    //        FPS = GameObject.Find("FPSController");
    //        Destroy(FPS);
    //        Destroy(null);
    //        Debug.Log("Found another one" + FPS);
    //    }
    //}
}
