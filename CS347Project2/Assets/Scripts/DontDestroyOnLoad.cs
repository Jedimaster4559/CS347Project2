using UnityEngine;

/// <summary>
/// This class prevents this game object from being destroyed when things
/// Get unloaded.
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
