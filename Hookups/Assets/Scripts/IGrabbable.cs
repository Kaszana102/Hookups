using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    void grab(Transform objectGrabPointTransform, PickupDrop player);
    void drop();
    void throwObject(DamageableObject damageableObject);
}
