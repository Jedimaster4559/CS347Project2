using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TutorialManager TutorialManager;
    public GameObject T_Movement;
    public GameObject T_Throwing;
    public GameObject T_Killing;

    public bool MoveVis = true;
    public bool ThrowVis = true;
    public bool KillVis = true;

    private void Update()
    {
        if (TutorialManager.FirstMovement == true)
        {
            MoveVis = false;

        }

        if (TutorialManager.FirstThrow == true)
        {
            ThrowVis = false;
        }

        if (TutorialManager.FirstBlood == true)
        {
            KillVis = false;
        }

        if (MoveVis == false)
        {
            Destroy(GameObject.FindWithTag("Tutorial_Move"));
        }

        if (ThrowVis == false)
        {
            Destroy(GameObject.FindWithTag("Tutorial_Throw"));
        }

        if (KillVis == false)
        {
            Destroy(GameObject.FindWithTag("Tutorial_Kill"));
        }
    }
}
