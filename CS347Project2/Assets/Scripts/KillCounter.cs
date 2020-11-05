using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public static int counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }

    public static void kill()
    {
        counter++;
    }
}
