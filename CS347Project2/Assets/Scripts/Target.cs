using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target = null;
    public GameObject target2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("fuck my ass");
        if (collision.gameObject == target2)
            target = collision.gameObject;
        
    }
    
}