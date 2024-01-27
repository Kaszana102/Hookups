using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IGrabbable, IThrowable, IDamaging
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    private Collider collider;
    public DamageableObject damageableObject;
    [SerializeField] public float lerpSpeed =32;
    [SerializeField] public float throwForce = 1000;


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

    public void throwObject(DamageableObject damageableObject)
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 forceVector = objectGrabPointTransform.forward.normalized*throwForce;
            this.damageableObject = damageableObject;
            drop();
            rb.AddForce(forceVector, ForceMode.Impulse);
        }
        
    }

    public int howMany()
    {
        return 4;
    }

    public IDamageable thrower()
    {
        return this.damageableObject;
    }
}
