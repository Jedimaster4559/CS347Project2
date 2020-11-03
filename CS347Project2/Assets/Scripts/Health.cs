using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to manage the health of a game object.
/// 
/// Most of the time, the object this script is attached
/// to will need some kind of collider as well.
/// </summary>
public class Health : MonoBehaviour
{
    // Health Starting Configuration
    public float maxHealth = 100;
    public float startingHealth = 100;

    // Health Detail
    [SerializeField]
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            OnDeath();
        }
    }

    /// <summary>
    /// Applies damage to this health object
    /// </summary>
    /// <param name="amount">The amount of damage to apply</param>
    public void Damage(float amount)
    {
        currentHealth -= amount;
    }

    /// <summary>
    /// Applies healing to this health object
    /// </summary>
    /// <param name="amount">The amount of healing to apply</param>
    public void Heal(float amount)
    {
        currentHealth += amount;

        // If the health is higher than the maximum amount of health,
        // we should only have the maximum amount of health.
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    /// <summary>
    /// Method that gets called when a players health drops below 0
    /// 
    /// Note: this default behaviour may not be desired for all entities.
    /// (For example, the player or a boss should likely have some unique
    /// things happen upon death). If this is the case, extend this class
    /// and then overload this method to redefine it's behaviour.
    /// </summary>
    public void OnDeath()
    {
        // When an object health gets to less than 0, it should
        // be destroyed.
        Destroy(this.gameObject);
    }
}
