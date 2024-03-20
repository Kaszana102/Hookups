using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikyBall : ObjectGrabbable
{
    public override void grab(Transform objectGrabPointTransform, DamageableObject thrower, PickupDrop player)
    {
        base.grab(objectGrabPointTransform, thrower, player);                        
        thrower.receiveHoldingDamage(1);
    }
}
