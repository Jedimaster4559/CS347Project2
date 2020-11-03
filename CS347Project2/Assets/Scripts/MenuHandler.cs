﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< HEAD
/// <summary>
/// Handles the menus and the user's interactions with them.
/// </summary>
=======
>>>>>>> master
public class MenuHandler : MonoBehaviour
{
    // Private State Variables
    private bool showPauseMenu = false;
    private bool showOptionsMenu = false;
    private bool showCreditsMenu = false;

    // Contains the name of the scene with the first level
    public string firstLevel;

    // Menu Objects
    public GameObject creditsMenu;
<<<<<<< HEAD
    public GameObject pauseMenu;
    public GameObject optionsMenu;
=======
>>>>>>> master


    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
        ProcessInput();
    }

    /// <summary>
    /// Processes a users input. Tries to determine the context
    /// so that it will either exit open menus or display the pause
    /// menu.
    /// </summary>
=======
        
    }

>>>>>>> master
    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
<<<<<<< HEAD
            // Hide any open menus
            bool contextFound = false;
            if (showCreditsMenu)
            {
                ToggleCredits();
                contextFound = true;
            }
            if (showPauseMenu)
            {
                ResumeGame();
                contextFound = true;
            }

            // Open the pause menu if we didn't leave a menu
            if (!contextFound)
            {
                PauseGame();
            }
=======
            // TODO: Add this implementation on pause menu implementation
>>>>>>> master
        }
    }

    /// <summary>
    /// Quits the game to the Desktop
    /// </summary>
    public void QuitToDesktop()
    {
        Application.Quit();
    }

    /// <summary>
    /// Returns the Game to the main menu.
    /// </summary>
    public void QuitToMain()
    {
<<<<<<< HEAD
        if (showPauseMenu){
            ResumeGame();
        }
=======
>>>>>>> master
        SceneManager.LoadScene("Start");
    }

    /// <summary>
    /// Loads the Game into the first level
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(firstLevel);
    }

    /// <summary>
    /// Toggles showing the credits menu
    /// </summary>
    public void ToggleCredits()
    {
        showCreditsMenu = !showCreditsMenu;
        creditsMenu.SetActive(showCreditsMenu);
    }
<<<<<<< HEAD

    /// <summary>
    /// Pauses the game.
    /// </summary>
    public void PauseGame()
    {
        // Pauses time scale so the game freezes.
        Time.timeScale = 0;
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
    }

    /// <summary>
    /// Resumes the game.
    /// </summary>
    public void ResumeGame()
    {
        // Resets the time scale to allow gameplay to
        // continue.
        Time.timeScale = 1;
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
    }
=======
>>>>>>> master
}
