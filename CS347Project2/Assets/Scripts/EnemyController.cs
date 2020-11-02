﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script to define enemy. 
/// Attaching this script to a game object can represent it as a simple enemy which has random rotation, stay still, and with a viewcone attached to it
/// 
/// This script requires the game object to have a rigidbody2D and a box collider2D.
/// 
/// </summary>
/// <author>Yingren Wang</author>

public class EnemyController : MonoBehaviour
{
    // Hidden Components
    [SerializeField] 
    private FieldOfView viewCone;

    private Rigidbody2D rb2d;
    private BoxCollider2D bc2d;

    private Vector3 aimDir;

    private float tillNextConeDirTime = 0.0f;
    public float coneDirPeriod = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        viewCone.transform.parent = this.transform;
        viewCone.SetOrigin(transform.position);
        aimDir = new Vector3(-1.0f, 0.0f, 0.0f);
        tillNextConeDirTime = coneDirPeriod;
    }
    
    /// <summary>
    /// Generate a random direction
    /// </summary>
    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        //aimDir = (Input.mousePosition - this.transform.position).normalized;
        viewCone.SetAimDirection(aimDir);
        
        tillNextConeDirTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (tillNextConeDirTime <= 0)
        {
            aimDir = GetRandomDir();
            tillNextConeDirTime = coneDirPeriod;
        }
    }
}