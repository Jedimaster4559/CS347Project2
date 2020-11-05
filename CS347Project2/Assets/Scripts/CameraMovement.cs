
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    // The target object to follow
    public GameObject target;
    public GameObject deathScreen;
    public Camera cam;
    public Text countDisplay;
    public float stickyFactor = 2;

    // Margins to represent how close to the edge the player can get
    // to the edge. These values are as percentages. That means that
    // if you set the value to 0.25, the player will be free to move
    // in the middle 50% of the screen, however the camera will move
    // towards them if they leave that area.
    public float verticalMargin = 0.4f;
    public float horizontalMargin = 0.4f;
    public float max_speed = 5;
    // The square bounds of the bounding box
    private float topBound;
    private float bottomBound;
    private float leftBound;
    private float rightBound;
    public float camera_speed = 1.0f;//Simple float for rate
    private Rigidbody2D rigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the camera component connected to this script
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        // Camera should start centered on the targe object
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        // Configuring Bounds of our camera box
        topBound = Screen.height - (Screen.height * verticalMargin);
        bottomBound = Screen.height * verticalMargin;
        rightBound = Screen.width - (Screen.width * horizontalMargin);
        leftBound = Screen.width * horizontalMargin;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            HandleDeath();
        }
        KeepInBorders();
    }

    /// <summary>
    /// Moves the camera to keep the player within the bounds of the
    /// camera's bounding box
    /// </summary>
    private void KeepInBorders()
    {
        // Getting the target position in respect to the screen
        Vector3 playerScreenPosition = cam.WorldToScreenPoint(target.transform.position);
        Vector2 updateVector = new Vector2(0, 0);
        
        // Handle Right Bound
        if (playerScreenPosition.x > rightBound)
        {
            updateVector.x = camera_speed;

        }

        // Handle Left Bound
        else if (playerScreenPosition.x < leftBound)
        {
            updateVector.x = -camera_speed;
        }
        else
        {
            updateVector.x = 0;
        }

        // Handle Top Bound
        if (playerScreenPosition.y > topBound)
        {
            updateVector.y = camera_speed;
        }

        // Handle Bottom Bound
        else if (playerScreenPosition.y < bottomBound)
        {
            updateVector.y = -camera_speed;
        }
        else
        {
            updateVector.y = 0;//This prevents the camera from bouncing around endlessly
        }
        //This block makes sure the camera doesn't move faster than the player, but not slower either, it keeps the jitters away
        if (rigidBody.velocity.x > max_speed)
        {
            rigidBody.velocity = new Vector2(max_speed, rigidBody.velocity.y);
        }
        else if (rigidBody.velocity.x < -max_speed)
        {
            rigidBody.velocity = new Vector2(-max_speed, rigidBody.velocity.y);
        }
        if (rigidBody.velocity.y > max_speed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, max_speed);
        }
        else if (rigidBody.velocity.y < -max_speed)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x,- max_speed);
        }
        rigidBody.velocity += updateVector * (stickyFactor / 10);//Updates the velocity of the camera
        
    }

    void HandleDeath()
    {
        deathScreen.SetActive(true);
        countDisplay.text = "Guard Kill Count: " + KillCounter.counter;
        target = this.gameObject;
        Invoke("RestartScene", 15f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}