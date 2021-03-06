﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AssignPauseMenuButtons : MonoBehaviour
{
    private GameManager gameManager = GameManager.instance;
    private Assets.Scripts.PauseMenu pauseMenu;
    
    private Button resume;
    private Button restartLevel;
    private Button mainMenu;

    private void Start()
    {
        //gameManager = FindObjectOfType<GameManager>();
        AssignButtons();
    }

    private void AssignButtons()
    {
        Button[] buttons = GetComponent<Canvas>().GetComponentsInChildren<Button>(true);

        pauseMenu = FindObjectOfType<Canvas>().GetComponent<Assets.Scripts.PauseMenu>();

        resume = buttons[0];
        restartLevel = buttons[1];
        mainMenu = buttons[2];

        resume.onClick.AddListener(delegate
        {
            pauseMenu.TogglePause();
        });
        restartLevel.onClick.AddListener(delegate
        {
            gameManager.RestartLevel();
            gameManager.ToggleTimeScale();
        });
        mainMenu.onClick.AddListener(delegate
        {
            gameManager.ToggleTimeScale();
            gameManager.LoadMainMenu();
        });
    }
}
