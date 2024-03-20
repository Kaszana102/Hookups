using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    public bool touchingWall;
    private float lastDashed = 0;
    private int availbleJumps = 0;
    public Camera camera;
    private Vector2 look,moveDirection,movInterpolated=Vector2.zero;
    private float lookRotation;
    private bool sprinting;
    [SerializeField]
    [Range(0.01f,0.2f)]
    float speedingUp = 0.2f;

    float runStartTime = 0;
    bool running = false;
    bool dashing = false;
    Vector3 dashingDirection = Vector3.zero;    

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private ObjectGrabbable self;
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 prevVal = moveDirection;
        moveDirection = context.ReadValue<Vector2>();                
        Debug.Log("PRESSED");
        if (!sprinting)
        {
            if((!context.started || context.performed) ^context.canceled){                
                animator.SetInteger("MovSpeed", 1);
                running = true;
                if(prevVal.x ==0 && prevVal.y == 0)
                {
                    runStartTime = Time.time;
                }
                
            }
            else
            {
                running = false;
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
            dashing = true;
            dashingDirection = forward.normalized;
            //rb.AddForce(forward);
        }
    }

    void Update()
    {        
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            rb.MovePosition(transform.position + dashingDirection);
            if (Time.timeAsDouble > lastDashed + 0.5f)
            {
                dashing = false;
            }
        }
        else
        {
            if (running)
            {
                movInterpolated = Vector2.Lerp(movInterpolated, moveDirection, 0.2f);
            }
            else
            {
                movInterpolated = Vector2.Lerp(movInterpolated, Vector2.zero, 0.2f);
            }
            mov(movInterpolated);
            rb.AddForce(Vector3.down * 5);//gravity
        }
        
        if (self.grounded)
        {
            availbleJumps = 1;
            animator.SetBool("Jumping", false);
        }
        
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
               
    }

    void mov(Vector2 direction)
    {
        if (!self.thrown) {            
            Vector3 moveDirecetion = new Vector3(direction.x, 0, direction.y);
            moveDirecetion = transform.TransformDirection(moveDirecetion);
            if (sprinting)
            {
                moveDirecetion *= (speed * 2f);
            }
            else
            {
                moveDirecetion *= speed;
            }
            moveDirecetion *= (Time.time > runStartTime + 0.5f) ? 1 : (Time.time - runStartTime)/0.5f;
            
            rb.MovePosition(transform.position + moveDirecetion);
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

    private void OnCollisionEnter(Collision collision)
    {
        dashing = false;
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        dashing = false;
    }
    */
}
