using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public ObjectGrabbable grabbable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == grabbable.gameObject)
            return;
        
        grabbable.grounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == grabbable.gameObject)
            return;
        
        grabbable.grounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == grabbable.gameObject)
            return;
        
        grabbable.grounded = true;
    }
}
