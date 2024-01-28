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
    public bool touchingWall;
    private float lastDashed = 0;
    private int availbleJumps = 0;
    public Camera camera;
    private Vector2 move, look;
    private float lookRotation;
    private bool sprinting;

    [SerializeField]
    private Animator animator;

    private bool thrown = false;

    //IK

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        if (!sprinting)
        {
            if((!context.started || context.performed) ^context.canceled){
                animator.SetInteger("MovSpeed", 1);
            }
            else
            {
                animator.SetInteger("MovSpeed", 0);
            }
            
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        sprinting = (!context.started || context.performed) ^ context.canceled;
        if (sprinting)
        {
            animator.SetInteger("MovSpeed", 2);
        }
        else
        {
            animator.SetInteger("MovSpeed", 0);
        }           
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jmp();
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && Time.time > lastDashed + 3f)
        {
            lastDashed = Time.time;
            Vector3 forward = transform.forward * 10000;

            rb.AddForce(forward);
        }
    }

    void Update()
    {
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

    void mov()
    {
        if (!thrown) {
            Vector3 currentVelocity = rb.velocity;
            Vector3 targetVelocity = new Vector3(move.x, 0f, move.y);
            if (sprinting)
            {
                targetVelocity *= (speed * 2f);
            } else {
                targetVelocity *= speed;
            }


            targetVelocity = transform.TransformDirection(targetVelocity);

            Vector3 velocityChange = targetVelocity - currentVelocity;
            velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);

            Vector3.ClampMagnitude(velocityChange, maxForce);

            rb.AddForce(velocityChange, ForceMode.VelocityChange);
        }
    }

    void jmp()
    {
        Vector3 jumpForces = Vector3.zero;
        
        if (availbleJumps > 0)
        {
            availbleJumps--;
            animator.SetBool("Jumping",true);
            jumpForces = Vector3.up * jumpForce;
        }
        rb.AddForce(jumpForces,ForceMode.VelocityChange);
    }

    public void setGrounded(bool st)
    {
        grounded = st;
        if (st)
        {
            availbleJumps = 1;
            animator.SetBool("Jumping", false);
        }
    }

    public void setThrown(bool st)
    {
        thrown = st;
    }
    
}
