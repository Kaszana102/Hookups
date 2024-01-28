using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    private ObjectGrabbable grabbedItem;

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
                    grabbedItem = grabbable;
                    grabbedItem.damageableObject = GetComponent<DamageableObject>();
                    grabbedItem.grab(objectGrabPointTransform, grabbedItem.damageableObject);
                    animator.SetBool("Holding", true);
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
            grabbedItem = null;
        }
    }

    public void OnReleaseHand(InputAction.CallbackContext context)
    {
        if(grabbedItem!=null)
        {
            grabbedItem.drop();
            animator.SetBool("Holding", false);
            grabbedItem = null;
        }
    }
}
