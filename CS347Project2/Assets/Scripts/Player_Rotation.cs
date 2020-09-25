using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    private Vector2 current_pos = new Vector2(0, 0);
    private Vector2 previous_pos = new Vector2(0, 0);
    private Rigidbody2D rigidBody;
    private Vector2 change = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {   rigidBody = gameObject.GetComponent<Rigidbody2D>(); //Getting rigid body of player
        previous_pos = gameObject.GetComponent<Rigidbody2D>().position; //Gets the position of the previously declared rigid body
        current_pos = gameObject.GetComponent<Rigidbody2D>().position; //Gets the position of the previously declared rigid body
    }

    // Update is called once per frame
    void Update()
    {
        current_pos = gameObject.GetComponent<Rigidbody2D>().position;//Gets the current position of the rigid body
        change = current_pos - previous_pos; //Uses the position of the current frame and previous frame, finds the difference between them

        if (change.x > 0)//If the player is moving right
        {
            if (change.y == 0)//If the player isn't moving up or down
            {
                rigidBody.SetRotation(0);//Player is rotated directly right
            }
            else if (change.y > 0)//If player is moving up and right
            {
                rigidBody.SetRotation(45);//Player is rotated at a right and up diagonal
            }
            else if (change.y < 0)//If player is moving down and right
            {
                rigidBody.SetRotation(315);//Player is rotated at a right and down diagonal
            }
        }
        else if (change.x < 0)//If the player is moving left
        {
            if (change.y == 0)//If the player isn't moving up or down
            {
                rigidBody.SetRotation(180);//Set the player directly left
            }
            else if (change.y > 0)//If player is moving up
            {
                rigidBody.SetRotation(135);//Sets rotation diagonally between up and left
            }
            else if (change.y < 0)//If player is moving down
            {
                rigidBody.SetRotation(225);//Sets rotation diagonally between down and left
            }
        }
        else//If the player isn't moving left or right
        {
            if (change.y > 0)//If player is moving up
            {
                rigidBody.SetRotation(90);//Set rotation facing up
            }
            else if (change.y < 0)//If player is moving down
            {
                rigidBody.SetRotation(270);//Set rotation facing down
            }
        }
        previous_pos = current_pos;//This sets previous position to current position before next frame to start the cycle again
    }
}
