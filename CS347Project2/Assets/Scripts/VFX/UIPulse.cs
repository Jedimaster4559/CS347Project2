using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPulse : MonoBehaviour
{
    // The maximum size the object grows to
    public float maxScale;
    public float minScale;

    // The duration in seconds to grow
    public float speed = 1;

    // internal state
    private bool growing = false;
    private float timer = 0;
    private Vector3 minActual;
    private Vector3 maxActual;

    // Start is called before the first frame update
    void Start()
    {
        minActual = transform.localScale * minScale;
        maxActual = transform.localScale * maxScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Update for current frame
        timer += Time.deltaTime;
        UpdateState();

        // Calculate new size percentage
        float percent = timer / speed;
        if (!growing)
        {
            percent = 1 - percent;
        }

        Debug.Log(percent);

        // Calculate Size and Scale Object
        transform.localScale = ((maxActual - minActual) * percent) + minActual;
    }

    /// <summary>
    /// Updates the state of the Pulsing if it needs to grow or shrink.
    /// </summary>
    private void UpdateState()
    {
        if(timer > speed && growing)
        {
            growing = false;
            timer = 0;
        } else if (timer > speed && !growing)
        {
            growing = true;
            timer = 0;
        }
    }
}
