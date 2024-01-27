using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IGrabbable
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    private Collider collider;
    [SerializeField] public float lerpSpeed;
    [SerializeField] public float throwForce;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        collider.enabled = false;
    }

    public void drop() 
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
        collider.enabled = true;
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            var newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position,Time.deltaTime*lerpSpeed);
            rb.MovePosition(newPosition);
            rb.useGravity = false;
        }
    }

    public void throwObject()
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 forceVector = objectGrabPointTransform.forward.normalized*throwForce;
            drop();
            rb.AddForce(forceVector, ForceMode.Impulse);
        }
        
    }
}
