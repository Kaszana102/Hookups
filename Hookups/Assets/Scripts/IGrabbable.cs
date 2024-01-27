using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    void grab(Transform objectGrabPointTransform);
    void drop();
}
