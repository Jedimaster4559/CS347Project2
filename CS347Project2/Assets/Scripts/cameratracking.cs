using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameratracking : MonoBehaviour
{
    public Transform playerCharacter;

        private void FixedUpdate()
        {
            transform.position = new Vector3(playerCharacter.position.x, playerCharacter.position.y, transform.position.z);
        }
}

