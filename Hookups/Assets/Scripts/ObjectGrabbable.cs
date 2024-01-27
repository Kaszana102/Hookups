using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IGrabbable
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    [SerializeField] public float lerpSpeed;
    [SerializeField] public float throwForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public void grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
    }

    public void drop() 
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
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
