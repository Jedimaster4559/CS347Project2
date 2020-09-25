using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 forward = new Vector2(0, 0);
    // enum for facing
    public enum Facing { LEFT, RIGHT };

    // Hidden publics
    [HideInInspector]
    public Facing facing;

    // Variables
    public float moveSpeed = .1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
       
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        forward = new Vector3(horizontalInput, verticalInput,0);

        transform.position += forward*moveSpeed;
        //transform.Translate(forward.normalized * moveSpeed * Time.deltaTime);
    }
}
