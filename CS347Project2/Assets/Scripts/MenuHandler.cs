using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject pauseMenu;
    public GameObject optionsMenu;


    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
        if (showPauseMenu){
            ResumeGame();
        }
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        showPauseMenu = !showPauseMenu;
        pauseMenu.SetActive(showPauseMenu);
    }
}
