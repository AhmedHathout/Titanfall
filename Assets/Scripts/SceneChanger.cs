using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private AudioManager audioManager;
    public static GameManager gameManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Level1()
    {
        audioManager.Play("Game");
        SceneManager.LoadScene("Level1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void MainMenu()
    {
        audioManager.Play("Main Menu");
        SceneManager.LoadScene("MainMenu");
        audioManager.AddSliders();
    }

    public void GameOver()
    {
        audioManager.Play("Main Menu");
        SceneManager.LoadScene("GameOver");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Loadout()
    {
        SceneManager.LoadScene("Loadout Screen");
    }

}
