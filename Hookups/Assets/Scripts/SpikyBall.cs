using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyBall : ObjectGrabbable
{
    public override void grab(Transform objectGrabPointTransform, DamageableObject thrower)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        colliders.SetActive(false);
        grabAudioSourc.Play();
        rb.isKinematic = true;
        thrower.receiveHoldingDamage(this, tag);
    }
}
