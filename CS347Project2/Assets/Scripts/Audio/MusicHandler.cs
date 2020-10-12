using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This Class will long term handle the processing of multiple audio files.
/// For now, this just makes sure that the audio engine never gets re-instantiated
/// </summary>
public class MusicHandler : MonoBehaviour
{
    private static MusicHandler instance = null;

    // Start is called before the first frame update
    void Start()
    {
        // If a music handler already has been created, we need to
        // destroy this one.
        if(instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
