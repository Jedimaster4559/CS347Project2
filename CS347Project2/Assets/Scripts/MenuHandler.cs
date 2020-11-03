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


    // Update is called once per frame
    void Update()
    {
        
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO: Add this implementation on pause menu implementation
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
}
