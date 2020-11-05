using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This code handles getting input for the creation and deletion of
 all the required tutorial UI popups and other
 functions related to it */
public class TutorialManager : MonoBehaviour
{
    // public GameObject[] tutorialMenus;
    // public GameObject[] uiCanvases;
    public GameObject tutorialObject;
    public GameObject tutorialEnemy;
    public GameObject tutorialUI;
    public GameObject introDisplay;
    public GameObject exitDisplay;
    public string nextScene;

    public bool FirstMovement = false;
    public bool FirstThrow = false;
    public bool FirstBlood = false;

    public float startTime = 15;
    public float endTime = 15;

    private float unscaledTime = 0;
    private float endTimeRemaining = 0;

    void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        unscaledTime += Time.unscaledDeltaTime;

        if(unscaledTime > startTime)
        {
            introDisplay.SetActive(false);
            tutorialUI.SetActive(true);
            Time.timeScale = 1;
        }

        if(FirstBlood && FirstMovement && FirstThrow)
        {
            endTimeRemaining += Time.unscaledDeltaTime;
            Time.timeScale = 0;
            exitDisplay.SetActive(true);
            if (endTimeRemaining > endTime)
            {
                ChangeScene();
            }
        }

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D))
        {
            FirstMovement = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            FirstThrow = true;
          
            /*tried to raycast but it wouldn't work*/
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.name == tutorialObject.name)
                {
                    FirstThrow = true;
                }
            }
        }

        if (tutorialEnemy == null)
        {
            FirstBlood = true;
        }
    }

    public void ChangeScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }
}
        
    


