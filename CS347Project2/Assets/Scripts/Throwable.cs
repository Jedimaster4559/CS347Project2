﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to define what a throwable object is. Attaching this script
/// to a game object will make it throwable.
/// 
/// This script will add a rigidbody and box collider to this game object
/// if they do not already exists. If they do already exist, then is will
/// use the pre-existing ones.
/// 
/// If you intend to add special behaviours to throwable, such as implementing
/// being able to use a throwable object while interacting with it, extend
/// this class and then implement any of the following three functions:
/// - OnSpecialActionStart()
/// - OnSpecialActionStop()
/// - OnSpecialAction()
/// All other behaviours of Throwable will be inherited and will not need
/// to be re-implemented.
/// </summary>
/// <author>Nathan Solomon</author>
public class Throwable : MonoBehaviour
{
    // Hidden Components
    public Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    // Internal State Elements
    private bool isSelected = false;
    private bool mouseHovering = false;
    private bool specialAction = false;
    private float damage = 0;
    public float max_speed = 25;
    private Vector2 curve_vector = new Vector2(0, 0);
    private Vector2 o_vector = new Vector2(0, 0);

    // Public Elements
    public float mass = 1;
    public float suctionFactor = 2;
    public float rotationFactor = 1;
    public float angularDrag = 1;
    public float linearDrag = 1;
    public float speedBoost = 2.0f;

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
        if (boxCollider == null)
        {
            // Creating Box Collider
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

    }

    void Update()
    {
        HandleInput();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
        CalculateDamage();
    }

    // Called Whenever a mouse enters of this object
    void OnMouseEnter()
    {
        mouseHovering = true;
    }

    // Called Whenever a mouse is exiting the object
    void OnMouseExit()
    {
        mouseHovering = false;
    }

    /// <summary>
    /// Handles all user input related to throwable objects
    /// </summary>
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
                rigidBody.AddForce(rigidBody.velocity * speedBoost, ForceMode2D.Impulse);
                o_vector = rigidBody.velocity;
                isSelected = false;

                // Handles stopping special actions if they are occuring at this point in time
                if (specialAction)
                {
                    OnSpecialActionStop();
                }
                specialAction = false;
            }

        }

        // Handle Rotation of the object
        if (Input.GetAxis("Rotate Object") != 0 && isSelected)
        {
            float rotation = Input.GetAxis("Rotate Object");
            rigidBody.AddTorque(-rotation * rotationFactor);
        }

        // Handle Special Actions
        if (Input.GetAxis("Jump") == 0 && isSelected)
        {
            if (specialAction)
            {
                OnSpecialActionStop();
            }
            specialAction = false;
        }
        else if (Input.GetAxis("Jump") != 0 && isSelected)
        {
            if (!specialAction)
            {
                OnSpecialActionStart();
                specialAction = true;
            }

            OnSpecialAction();
        }

    }

    /// <summary>
    /// A function that can be overridden to simplify implementing
    /// special behaviours. This function will be called the frame
    /// where the special action button is clicked.
    /// </summary>
    public void OnSpecialActionStart()
    {

    }

    /// <summary>
    /// A function that can be overridden to simplify implementing
    /// special behaviours. This function will be called the frame
    /// where the special action button is released.
    /// </summary>
    public void OnSpecialActionStop()
    {

    }

    /// <summary>
    /// A function that can be overridden to simplify implementing
    /// special behaviours. This function will be called the frames
    /// where the special action button is held, including the frame
    /// when it is first pressed.
    /// </summary>
    public void OnSpecialAction()
    {

    }

    /// <summary>
    /// Handles this objects movement
    /// </summary>
    private void HandleMovement()
    {
        // If the object is selected, we should add force towards the mouse
        // based off of how far away the mouse is.
        if (isSelected)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 vectorToTarget = mousePosition - gameObject.transform.position;
            if (rigidBody.velocity.magnitude<max_speed*2)
                rigidBody.AddForce(vectorToTarget * suctionFactor * 2, ForceMode2D.Force);
        }
        else
        {
            curve_vector = new Vector2(-o_vector.y, o_vector.x); // This gets a vector from the release point, and flips it 90 degrees
            rigidBody.AddForce(rigidBody.angularVelocity / 1000 * curve_vector, ForceMode2D.Force); // This uses that curve vector, the direction of angular velocity to determine curve direction
            if (rigidBody.velocity.magnitude < mass)
            {
                rigidBody.velocity = new Vector2(0, 0);
                rigidBody.angularVelocity = 0;
            }
        } // Then uses that along with a flat devisor to reduve curve power, multiplies it all together to determine the smooth curve execution of the release
    }

    /// <summary>
    /// Calculates and sets the amount of damage this object will damage when it collides with
    /// a damageable object.
    /// </summary>
    void CalculateDamage()
    {
        damage = (rigidBody.velocity.magnitude * mass + Mathf.Abs(rigidBody.angularVelocity) / 200 * mass)/2;
    }

    // Called when this object collides with another game object that has collision enabled.
    void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.Damage(damage);
        }
    }


}
