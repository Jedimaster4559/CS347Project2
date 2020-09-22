using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to define what a throwable object is. Attaching this script
/// to a game object will make it throwable.
/// 
/// This script will add a rigidbody and box collider to this game object
/// if they do not already exists. If they do already exist, then is will
/// use the pre-existing ones.
/// </summary>
/// <author>Nathan Solomon</author>
public class Throwable : MonoBehaviour
{
    // Hidden Components
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    // Internal State Elements
    private bool isSelected = false;
    private bool mouseHovering = false;

    // Public Elements
    public float mass = 1;
    public float suctionFactor = 1;
    public float rotationFactor = 1;
    public float angularDrag = 1;
    public float linearDrag = 1;
    public float speedBoost = 7;

    // Start is called before the first frame update
    void Start()
    {
        // Searching for Component
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        // Creating a new Rigid body if this object doesn't
        // already have one.
        if (rigidBody == null)
        {
            // Creating Rigid Body
            rigidBody = gameObject.AddComponent<Rigidbody2D>();

            // Configuring Object Properties
            rigidBody.gravityScale = 0;
            rigidBody.mass = mass;
            rigidBody.angularDrag = angularDrag;
            rigidBody.drag = linearDrag;
        }

        // Creating a new Box Collider if this object doesn't
        // already have one
        if(boxCollider == null)
        {
            // Creating Box Collider
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
    }

    void OnMouseEnter()
    {
        mouseHovering = true;
    }

    void OnMouseExit()
    {
        mouseHovering = false;
    }

    private void HandleInput()
    {
        

        // TODO: Long term, we should re-write to use better input systems
        // like the input manager or the input axis
        if (Input.GetMouseButtonDown(0) && mouseHovering)
        {
            isSelected = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isSelected)
            {
                // TODO: Balance this equation
                rigidBody.AddForce(Mathf.Log10(rigidBody.velocity.magnitude) * rigidBody.velocity * speedBoost, ForceMode2D.Impulse);
                isSelected = false;
            }
            
        }

        if (Input.GetAxis("Rotate Object") != 0 && isSelected)
        {
            float rotation = Input.GetAxis("Rotate Object");
            rigidBody.AddTorque(-rotation * rotationFactor); 
        }

    }

    private void HandleMovement()
    {
        // If the object is selected, we should add force towards the mouse
        // based off of how far away the mouse is.
        if (isSelected)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vectorToTarget = mousePosition - gameObject.transform.position;
            rigidBody.AddForce(vectorToTarget * suctionFactor, ForceMode2D.Force);
        } else
        {

        }
    }


}
