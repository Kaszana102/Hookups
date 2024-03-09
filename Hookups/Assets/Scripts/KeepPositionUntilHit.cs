using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPositionUntilHit : MonoBehaviour
{
    bool stationary = true;
    Vector3 position;

    [SerializeField] GameObject collidersObject;
    Rigidbody rb;
    Collider[] Colliders;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        Colliders =collidersObject.GetComponentsInChildren<Collider>();
        rb = GetComponent<Rigidbody>();
        CollidersAreTriggers(true);

        rb.isKinematic = true;
    }


    private void OnTriggerEnter(Collider other)    
    {
        if(other.gameObject.tag != "Ground")
        {
            AllowMoving();
        }        
    }

    public void AllowMoving()
    {
        CollidersAreTriggers(false);
        rb.isKinematic = false;
        Destroy(this);
    }

    void CollidersAreTriggers(bool isTrigger)
    {
        foreach (Collider collider in Colliders)
        {
            collider.isTrigger = isTrigger;
        }
    }

}
