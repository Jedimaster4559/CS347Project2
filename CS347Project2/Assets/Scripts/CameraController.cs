using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // The target object to follow
    public GameObject target;
    public Camera cam;

    // Margins to represent how close to the edge the player can get
    // to the edge. These values are as percentages. That means that
    // if you set the value to 0.25, the player will be free to move
    // in the middle 50% of the screen, however the camera will move
    // towards them if they leave that area.
    public float verticalMargin = 0.25f;
    public float horizontalMargin = 0.25f;

    // The square bounds of the bounding box
    private float topBound;
    private float bottomBound;
    private float leftBound;
    private float rightBound;

    // Start is called before the first frame update
    void Start()
    {
        // Get the camera component connected to this script
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }

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
        Vector3 updateVector = new Vector3(0, 0);

        // Handle Right Bound
        if(playerScreenPosition.x > rightBound)
        {
            updateVector.x = playerScreenPosition.x - rightBound;
            
        }

        // Handle Left Bound
        if(playerScreenPosition.x < leftBound)
        {
            updateVector.x = playerScreenPosition.x - leftBound;
        }

        // Handle Top Bound
        if(playerScreenPosition.y > topBound)
        {
            updateVector.y = playerScreenPosition.y - topBound;
        }

        // Handle Bottom Bound
        if(playerScreenPosition.y < bottomBound)
        {
            updateVector.y = playerScreenPosition.y - bottomBound;
        }

        transform.position += updateVector;
    }
}
