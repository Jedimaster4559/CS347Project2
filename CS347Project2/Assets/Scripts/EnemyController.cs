using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to define enemy. 
/// Attaching this script to a game object can represent it as a simple enemy which has random rotation, stay still, and with a viewcone attached to it
/// 
/// This script requires the game object to have a rigidbody2D and a box collider2D.
/// 
/// </summary>
/// <author>Yingren Wang</author>

public class EnemyController : MonoBehaviour
{
    // Hidden Components
    [SerializeField] 
    private FieldOfView viewCone;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    private Vector3 aimDir;

    public float rotateFrequency = 1;

    // Start is called before the first frame update
    void Start()
    {
        viewCone.transform.parent = this.transform;
        viewCone.SetOrigin(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        aimDir = new Vector3(0, -1, 0);
        viewCone.SetAimDirection(aimDir);
    }
}
