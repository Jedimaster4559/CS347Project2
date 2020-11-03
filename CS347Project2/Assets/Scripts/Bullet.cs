using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed = 20;
    public Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = GetComponentInParent<EE>().transform.rotation;
        rigidBody = this.GetComponent<Rigidbody2D>();
        rigidBody.velocity= GetComponentInParent<EE>().transform.right*speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CalculateDamage()
    {
        damage = 20;
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
