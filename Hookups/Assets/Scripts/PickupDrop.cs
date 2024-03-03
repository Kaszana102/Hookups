using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupDrop : ObjectGrabbable
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransformSource;    
    [SerializeField] private int visibleForPlayerMask;
    public ObjectGrabbable grabbedItem;

    [SerializeField]
    private Animator animator;

    public void OnGrab(InputAction.CallbackContext context) {
        float pickupDistance = 30f;
        ObjectGrabbable grabbable;
        RaycastHit raycastHit;
        
        if (grabbedItem != null)
            return;

        if (!Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out raycastHit, pickupDistance))
            return;
        
        if (!raycastHit.transform.TryGetComponent(out grabbable))
            return;
            
        if (player != null && raycastHit.transform == player.transform)
            return;

        if (grabbable == this) //check if grabbing self
            return;

        grabbedItem = grabbable;
        grabbedItem.damageableObject = GetComponent<DamageableObject>();
        grabbedItem.grab(objectGrabPointTransformSource, grabbedItem.damageableObject, this);
        animator.SetBool("Holding", true);        
        SetLayer(grabbedItem.transform,visibleForPlayerMask);
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (grabbedItem == null)
            return;

        grabbedItem.throwObject(GetComponent<DamageableObject>());
        animator.SetTrigger("Throw");
        animator.SetBool("Holding", false);
        SetLayer(grabbedItem.transform, 0);
        grabbedItem = null;

    }

    public void OnReleaseHand(InputAction.CallbackContext context)
    {

        if (grabbedItem == null)
            return;
        
        grabbedItem.drop();
        animator.SetBool("Holding", false);
        SetLayer(grabbedItem.transform,0);
        grabbedItem = null;
    }


    private void SetLayer(Transform target, int layer)
    {        
        foreach (Transform child in target.GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer=layer;
        }
    }
}
