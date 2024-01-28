using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IGrabbable
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    private DamageableObject damageableObject;
    protected PickupDrop player;
    public DamageableObject DamObj {get;set;}

    [SerializeField] public float lerpSpeed =32;
    [SerializeField] public float throwForce = 1000;
    [SerializeField] private GameObject colliders = null;

    [SerializeField]
    AudioClip grabAudio, throwAudio,dropAudio;
    AudioSource grabAudioSourc, throwAudioSource, dropAudioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (!colliders)
            colliders = transform.Find("Colliders").gameObject;
        
    }

    private void Start()
    {
        grabAudioSourc = gameObject.AddComponent<AudioSource>();
        throwAudioSource = gameObject.AddComponent<AudioSource>();
        dropAudioSource = gameObject.AddComponent<AudioSource>();

        grabAudioSourc.clip = grabAudio;
        throwAudioSource.clip = throwAudio;
        dropAudioSource.clip = dropAudio;
    }

    public void grab(Transform objectGrabPointTransform, PickupDrop player)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        colliders.SetActive(false);
        grabAudioSourc.Play();
        rb.velocity = Vector3.zero;
        this.player = player;
        rb.isKinematic = true;
    }

    public void drop() 
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
        colliders.SetActive(true);
        dropAudioSource.Play();
        player = null;
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

    public void throwObject(DamageableObject damageableObject)
    {
        if (objectGrabPointTransform != null)
        {
            Vector3 forceVector = objectGrabPointTransform.forward.normalized*throwForce;
            DamObj = damageableObject;
            drop();
            rb.AddForce(forceVector, ForceMode.Impulse);
            throwAudioSource.Play();
            player = null;
            rb.isKinematic = false;
        }
        
    }
}
