using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to define the player
/// Attaching this script to a game object can give the object player behavior
/// </summary>
/// <author>Yingren Wang</author>

public class PlayerController : MonoBehaviour
{
    // enum for facing
    public enum Facing { LEFT, RIGHT };

    // Hidden publics
    [HideInInspector]
    public Facing facing;

    // variables for moving
    public float moveSpeed = 0.1f;
    private Vector3 forward = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
       
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        forward = new Vector3(horizontalInput, verticalInput,0);

        transform.position += forward*moveSpeed;
    }
}
