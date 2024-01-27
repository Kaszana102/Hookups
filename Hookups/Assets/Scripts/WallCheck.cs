using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerMovement.gameObject)
        {
            return;
        }
        playerMovement.setTouchingWalls(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerMovement.gameObject)
        {
            return;
        }
        playerMovement.setTouchingWalls(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerMovement.gameObject)
        {
            return;
        }
        playerMovement.setTouchingWalls(true);
    }
}
