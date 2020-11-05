using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// A Script to controll bullet
/// Bullet attached with this controller will detect collision, if the bullet collides with anything other than enemy
///     and other bullets, the bullet gameObject will be destroyed.
/// 
/// This script requires the game object that needs to be hit to have a rigidbody2D and a collider2D.
/// 
/// Variables:
///     speed: bullet movement speed
///     shouldFlip: decide whether the sprite of the bullet should flip horizontally
///     bulletAllowedTime: time period that a bullet is allowed to exist even if it doesn't hit anything
/// </summary>
/// 
/// <author>
///     Kristian Wells
///     Yingren Wang
/// </author>

public class BulletController : MonoBehaviour
{
    // shooting bullet properties
    public float speed = 0.7f;
    public float bulletDamage = 7.0f;

    // GameObjects
    public GameObject enemy;
    public GameObject player;

    // whether should flip the sprite of bullet
    // variable is set depending on the relative position of enemy to the player
    public bool shouldFlip = false;

    // variable to destroy bullet after a certain time period
    [SerializeField]
    private float bulletAllowedTime = 10.0f;
    private float timeTillDestroy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = Resources.Load("Prefabs/EE") as GameObject;

        timeTillDestroy = bulletAllowedTime;
    }

    // Update is called once per frame
    void Update()
    {
        timeTillDestroy -= Time.deltaTime;
        if(timeTillDestroy <= 0)
        {
            // destroy the bullet after 10 seconds even if it hasn't hit anything
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        // bullets get shot towards the direction of the enemy facing
        var displacement = transform.right * speed;

        // set the bullet new position depending on enemy's position to the player's position
        // take a look at Scout.cs variable isToPlayersRight to check the logic of updating bool shouldFlip

        if (shouldFlip)
            transform.position -= displacement;
        else
            transform.position += displacement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CircleCollider2D collider = collision.otherCollider as CircleCollider2D;

        if (!collision.gameObject.GetComponent<Scout>() && !collision.gameObject.GetComponent<BulletController>())
        {
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                // minus 6.0 bullet damage if get hit
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.Damage(bulletDamage);
                }
            }
            // bullet will be destroied colliding into anything other than enemy and other bullets
            Destroy(this.gameObject);
        }
        else
        {
            // bullet won't be destroied colliding into enemy and other bullets
        }
    }
}
