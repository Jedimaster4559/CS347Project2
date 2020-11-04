using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public GameObject enemy;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.0f;
        // enemy = GameObject.Find("Enemy");
        // player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        // bullets get shot
        var displacement = transform.right * speed;
        var newPosition = transform.position - displacement;

        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BulletController>())
        {
            // bullets can touch other bullets
        }
        else if (collision.gameObject.GetComponent<Scout>())
        {
            // bullets can touch enemy
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != enemy.name)
        {
            Destroy(this);
        }
        else
        {

        }
    }
}
