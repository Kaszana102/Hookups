using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IGrabbable
{
    protected Rigidbody rb;
    protected Transform objectGrabPointTransform;
    protected GameObject colliders;
    public DamageableObject damageableObject;
    [SerializeField] public float lerpSpeed =32;
    [SerializeField] public float throwForce = 1000;

    [SerializeField]
    protected AudioClip grabAudio, throwAudio,dropAudio;
    protected AudioSource grabAudioSourc, throwAudioSource, dropAudioSource;
    public int damage = 1;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        colliders = transform.Find("Colliders").gameObject;
    }

    protected void Start()
    {
        grabAudioSourc = gameObject.AddComponent<AudioSource>();
        throwAudioSource = gameObject.AddComponent<AudioSource>();
        dropAudioSource = gameObject.AddComponent<AudioSource>();

        grabAudioSourc.clip = grabAudio;
        throwAudioSource.clip = throwAudio;
        dropAudioSource.clip = dropAudio;
    }

    public virtual void grab(Transform objectGrabPointTransform,DamageableObject thrower)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        colliders.SetActive(false);
        grabAudioSourc.Play();
        rb.isKinematic = true;
    }

    public virtual void drop() 
    {
        objectGrabPointTransform = null;
        rb.useGravity = true;
        colliders.SetActive(true);
        dropAudioSource.Play();
        rb.isKinematic = false;
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

    public virtual void throwObject(DamageableObject damageableObject)
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 forceVector = objectGrabPointTransform.forward.normalized*throwForce;
            this.damageableObject = damageableObject;
            drop();
            rb.AddForce(forceVector, ForceMode.Impulse);
            throwAudioSource.Play();
            rb.isKinematic = false;
        }
        
    }
}
