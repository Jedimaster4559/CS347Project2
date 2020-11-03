using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float top = 90;
    public float bottom = 180;
    private bool up = true;
    public float rate = 0.8f;
    public GameObject player;
    public float radius = 200;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.SetRotation(top);
    }

 
    


    // Update is called once per frame
    void Update()
    {

        var x = this.gameObject.GetComponent<Rigidbody2D>().position.x;
        var y = this.gameObject.GetComponent<Rigidbody2D>().position.y;
        var x2 = player.GetComponent<Rigidbody2D>().position.x;
        var y2 = player.GetComponent<Rigidbody2D>().position.y;
        radius = Mathf.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
        Vector2 toVector = player.transform.position - transform.position;
        float angleToTarget = Vector2.SignedAngle(transform.right, toVector);
        float test = Vector2.Angle(transform.right, toVector);
        if (radius>4 || test>45)
        {
            if (up == true && rigidBody.rotation <= top)
                up = false;
            if (up == false && rigidBody.rotation >= bottom)
                up = true;
            if (up == true)
                rigidBody.rotation -= rate;
            if (up == false)
                rigidBody.rotation += rate;
        }
        else
        {
            rigidBody.rotation += angleToTarget*rate/6;
            shoot();
        }
        
    }
    void shoot() { 
    
    }
}
    