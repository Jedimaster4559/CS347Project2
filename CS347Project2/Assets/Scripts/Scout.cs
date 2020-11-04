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

    public GameObject bulletPrefab;
    public float radius = 200;

    public float bulletAngle;
    public float shootingTime = 0.1f;

    // control how often to shoot
    private float projectilePeriod;
    private float timeTillNextProjectile;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.SetRotation(top);

        // set variables for shooting frequency
        projectilePeriod = 1.0f;
        timeTillNextProjectile = projectilePeriod;
    }

 
    


    // Update is called once per frame
    void Update()
    {
        timeTillNextProjectile -= Time.deltaTime;

        var x = this.gameObject.GetComponent<Rigidbody2D>().position.x;
        var y = this.gameObject.GetComponent<Rigidbody2D>().position.y;
        var x2 = player.GetComponent<Rigidbody2D>().position.x;
        var y2 = player.GetComponent<Rigidbody2D>().position.y;
        radius = Mathf.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
        Vector2 toVector = player.transform.position - transform.position;
        float angleToTarget = Vector2.SignedAngle(transform.right, toVector);
        float test = Vector2.Angle(transform.right, toVector);

        if (radius > 4 || test > 45)
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
            if (timeTillNextProjectile <= 0)
            {
                shoot();
                timeTillNextProjectile = projectilePeriod;
            }
        }
        
    }

    private void FixedUpdate()
    {
        // determine the angle of shooting the bullet
        var rise = this.transform.position.y - player.transform.position.y;
        var run = this.transform.position.x - player.transform.position.x;
        bulletAngle = Mathf.Rad2Deg * Mathf.Atan(rise / run);
    }

    void shoot() {
        //Determine directions using Euler angles
        Vector3 eulerAngle1 = new Vector3(0, 0, bulletAngle);

        //Convert to Quaternions for Unity use
        var quaternion1 = Quaternion.Euler(eulerAngle1);

        var bulletOffset = new Vector3(0.0f, 0.0f, 0.0f);
        //Create bullets
        var bullet = Instantiate(bulletPrefab, transform.position + bulletOffset, quaternion1);

        // assign player and enemy game objects to the initiated bullet
        var controller = bullet.GetComponent<BulletController>();
        controller.player = player;
        controller.enemy = this.gameObject;


        // bullet.transform.LookAt(player.transform);

        ////If bullets are fired facing left, need to mirror sprite
        //if (facing == Facing.LEFT)
        //{
        //    bullet1.GetComponent<SpriteRenderer>().flipY = false;
        //}
    }
}
    