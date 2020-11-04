using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton to keep the pause menu from getting reinstantiated on every scene change.
/// </summary>
public class PauseMenuSingleton : MonoBehaviour
{
    private static PauseMenuSingleton instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }

}
