using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float sensitivity;
    public float maxForce;
    public float jumpForce;
    public bool grounded;
    public Camera camera;
    private Vector2 move, look;
    private float lookRotation;


    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jmp();
    }

    private void FixedUpdate()
    {
        mov();
        
    }

    void LateUpdate()
    {
        transform.Rotate(Vector3.up*look.x*sensitivity);

        lookRotation += (-look.y*sensitivity);
        lookRotation = Mathf.Clamp(lookRotation,-90,90);

        camera.transform.eulerAngles = new Vector3(lookRotation,camera.transform.eulerAngles.y,camera.transform.eulerAngles.z);   
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void mov()
    {
            Vector3 currentVelocity = rb.velocity;
        Vector3 targetVelocity = new Vector3(move.x,0f, move.y);
        targetVelocity *= speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

        Vector3.ClampMagnitude(velocityChange,maxForce);

        rb.AddForce(velocityChange,ForceMode.Acceleration);
    }

    void jmp()
    {
        Vector3 jumpForces = Vector3.zero;

        if (grounded)
        {
            jumpForces = Vector3.up * jumpForce;
        }
        rb.AddForce(jumpForces,ForceMode.VelocityChange);
    }

    public void setGrounded(bool st)
    {
        grounded = st;
    }
}
