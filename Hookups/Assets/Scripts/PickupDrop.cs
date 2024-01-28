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

    public void OnGrab(InputAction.CallbackContext context){
        if (grabbedItem == null)
        {
            float pickupDistance = 30f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance))
            {
                Debug.Log(raycastHit);
                ObjectGrabbable grabbable;
                if (raycastHit.transform.TryGetComponent(out grabbable))
                {             
                    if(player == null || raycastHit.transform != player.transform)
                    {
                        grabbedItem = grabbable;
                        grabbedItem.damageableObject = gameObject.GetComponent<DamageableObject>();
                        grabbedItem.grab(objectGrabPointTransformSource, grabbedItem.damageableObject, this);
                        animator.SetBool("Holding", true);                        
                        SetLayer(grabbedItem.transform, visibleForPlayerMask);
                    }                    
                }
            }
        }        
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (grabbedItem != null){
            grabbedItem.throwObject(GetComponent<DamageableObject>());
            animator.SetTrigger("Throw");
            animator.SetBool("Holding", false);
            SetLayer(grabbedItem.transform, 0);
            grabbedItem = null;
        }
    }

    public void OnReleaseHand(InputAction.CallbackContext context)
    {
        if(grabbedItem!=null)
        {
            grabbedItem.drop();
            animator.SetBool("Holding", false);
            SetLayer(grabbedItem.transform, 0);
            grabbedItem = null;
        }
    }


    private void SetLayer(Transform target, int layer)
    {
        foreach(Transform child in target)
        {
            child.gameObject.layer=layer;
        }
    }
}
