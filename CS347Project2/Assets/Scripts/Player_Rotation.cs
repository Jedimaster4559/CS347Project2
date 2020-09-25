using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    private Vector2 pos1 = new Vector2(0, 0);
    private Vector2 pos2 = new Vector2(0, 0);
    private Rigidbody2D rigidBody;
    private Vector2 change = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        pos1 = gameObject.GetComponent<Rigidbody2D>().position;
    }

    // Update is called once per frame
    void Update()
    {
        pos1 = gameObject.GetComponent<Rigidbody2D>().position;
        change = pos1 - pos2;

        if (change.x > 0)
        {
            if (change.y == 0)
            {
                rigidBody.SetRotation(0);
            }
            else if (change.y > 0)
            {
                rigidBody.SetRotation(45);
            }
            else if (change.y < 0)
            {
                rigidBody.SetRotation(315);
            }
        }
        else if (change.x < 0)
        {
            if (change.y == 0)
            {
                rigidBody.SetRotation(180);
            }
            else if (change.y > 0)
            {
                rigidBody.SetRotation(135);
            }
            else if (change.y < 0)
            {
                rigidBody.SetRotation(225);
            }
        }
        else
        {
            if (change.y > 0)
            {
                rigidBody.SetRotation(90);
            }
            else if (change.y < 0)
            {
                rigidBody.SetRotation(270);
            }
        }
        pos2 = pos1;
    }
}
