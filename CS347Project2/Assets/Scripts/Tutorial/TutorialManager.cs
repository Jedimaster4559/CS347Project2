using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*This code handles getting input for the creation and deletion of
 all the required tutorial UI popups and other
 functions related to it */
public class TutorialManager : MonoBehaviour
{
    // public GameObject[] tutorialMenus;
    // public GameObject[] uiCanvases;
    public GameObject tutorialObject;
    public GameObject tutorialEnemy;

    public bool FirstMovement = false;
    public bool FirstThrow = false;
    public bool FirstBlood = false;
    private void Update()
    {

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
}
        
    


