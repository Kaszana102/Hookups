using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    void grab(Transform objectGrabPointTransform, DamageableObject thrower, PickupDrop player);
    void drop();
    void throwObject(DamageableObject damageableObject);
}
