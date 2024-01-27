using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickupDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    private IGrabbable grabbable;

    [SerializeField]
    private Animator animator;

    public void OnGrab(InputAction.CallbackContext context){
        if (grabbable == null)
            {
                float pickupDistance = 30f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance))
                {
                    Debug.Log(raycastHit);
                    if (raycastHit.transform.TryGetComponent(out grabbable))
                    {
                        grabbable.grab(objectGrabPointTransform);
                        animator.SetBool("Holding", true);
                }
                }
            }
            else
            {
                grabbable.drop();
            animator.SetBool("Holding", false);
            grabbable = null;
            }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (grabbable != null){
            grabbable.throwObject();
        }

    }
}
