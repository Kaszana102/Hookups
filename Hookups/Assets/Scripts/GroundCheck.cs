using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerMovement.gameObject)
        {
            return;
        }
        playerMovement.setGrounded(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerMovement.gameObject)
        {
            return;
        }
        playerMovement.setGrounded(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerMovement.gameObject)
        {
            return;
        }
        playerMovement.setGrounded(true);
    }
}
