using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to controll enemy
/// 
/// Enemy has random rotation with an invisible "viewCone"
///     if player gets into the cone, enemy will stop random rotation and focus on the player and start shooting
/// 
/// This script requires the game object that represents enemy to have a rigidbody2D.
/// 
/// Variables:
/// ---Rotation Related Variables---
/// 
/// ---Shoot Related Variables---
///     vecToPlayer: vector from enemy to player
///     isToPlayersRight: boolean value, if enemy is to player's right, true; otherwise, false
///     bulletOffset: offset for bullet respond position
///     projectilePeriod: time period between two shooting events
/// </summary>
/// 
/// <author>
///     Kristian Wells
///     Yingren Wang
/// </author>

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

    // bullet's angle when being initiated
    public float bulletAngle;

    // control how often to shoot
    private float projectilePeriod = 0.5f;
    private float timeTillNextProjectile;

    [SerializeField]
    private Vector3 bulletOffset;
    private bool isToPlayersRight;
    private Vector3 vecToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.SetRotation(top);

        // set variables for shooting frequency
        timeTillNextProjectile = projectilePeriod;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // update time for shooting
            timeTillNextProjectile -= Time.deltaTime;

            var x = this.gameObject.GetComponent<Rigidbody2D>().position.x;
            var y = this.gameObject.GetComponent<Rigidbody2D>().position.y;
            var x2 = player.GetComponent<Rigidbody2D>().position.x;
            var y2 = player.GetComponent<Rigidbody2D>().position.y;
            radius = Mathf.Sqrt((x - x2) * (x - x2) + (y - y2) * (y - y2));
            Vector2 toVector = player.transform.position - transform.position;
            float angleToTarget = Vector2.SignedAngle(transform.right, toVector);
            float viewCone = Vector2.Angle(transform.right, toVector);

            // viewCone functionality
            if (radius > 6 || viewCone > 60)
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
                // make the rotation less precise
                rigidBody.rotation += angleToTarget * rate / 6;

                // shoot every projectilePeriod time
                if (timeTillNextProjectile <= 0)
                {
                    shoot();
                    timeTillNextProjectile = projectilePeriod;
                }
            }

            // decide the position of enemy relative to player's position
            vecToPlayer = player.transform.position - transform.position;

            if (vecToPlayer.x <= 0)
            {
                isToPlayersRight = true;
            }
            else
            {
                isToPlayersRight = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            // determine the angle of shooting the bullet
            var rise = this.transform.position.y - player.transform.position.y;
            var run = this.transform.position.x - player.transform.position.x;
            bulletAngle = Mathf.Rad2Deg * Mathf.Atan(rise / run);
        }
    }

    private void OnDestroy()
    {
        KillCounter.kill();
    }

    void shoot() {
        // Determine directions using Euler angles
        Vector3 eulerAngle1 = new Vector3(0, 0, bulletAngle);

        // Convert to Quaternions for Unity use
        var quaternion1 = Quaternion.Euler(eulerAngle1);

        // generate bullet offset depending on vector to player
        var bulletOffset = new Vector3(vecToPlayer.x * 0.2f, vecToPlayer.y * 0.2f, 0.0f);

        // Create bullets
        var bullet = Instantiate(Resources.Load("Prefabs/Bullet"), transform.position + bulletOffset, quaternion1) as GameObject;
        if (isToPlayersRight)
        {
            bullet.GetComponent<BulletController>().shouldFlip = true;
            bullet.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            bullet.GetComponent<BulletController>().shouldFlip = false;
            bullet.GetComponent<SpriteRenderer>().flipX = false;
        }

        // assign player and enemy game objects to the initiated bullet
        var controller = bullet.GetComponent<BulletController>();
        controller.player = player;
        controller.enemy = this.gameObject;
    }
}
    