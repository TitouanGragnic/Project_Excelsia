using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator animator;
    float playerHeight = 2f;

    [SerializeField] Transform orientation;


    [Header("Movement")]
    public float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.5f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 50f;
    [SerializeField] public float sprintSpeed; //= 100f

    [Header("Crouching")]
    [SerializeField] float crouchSpeed = 30f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float camAcceleration = 10f;
    [SerializeField] float slideMinSpeed = 5f;
  


    [Header("Jumping")]
    public float jumpForce = 5f;
    float slideJump;
    [SerializeField] float slideJumpForce;



    [Header("Keybinds")]
    [SerializeField] KeyCode jumpkey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;

    int doubleJump = 1;
    public int nbJump;
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float slideDrag = 0.5f;
    [SerializeField] float airDrag = 0.5f;
    [SerializeField] float wallDrag = 1f;
    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    bool isSprinting;
    bool isSliding;
    bool isCrouching;
    int  moveVert = 1;
    public float camTranslate = 0f;
    float groundDistance = 1f;


    Vector3 moveDirection;
    Vector3 moveAirDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;



    private bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    void Awake()
    {
        nbJump = 1;
        sprintSpeed = 100f;
        
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        animator = GetComponentInChildren<Animator>();
    }
    
    private void Update()
    {

       

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isSprinting = Input.GetKey(sprintKey);
        isCrouching = Input.GetKey(crouchKey);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(jumpkey) && doubleJump > 0 && (!(wallLeft | wallright) | (isGrounded && (wallright | wallLeft))))
        {
            doubleJump -= 1;
            Jump();
        }
        if (isGrounded | wallLeft | wallright)
        {
            doubleJump = nbJump;
        }
        rb.useGravity = true;

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        CheckWall();

        if (CanWallrun())
        {
            if (wallLeft)
            {
                StartWallRun();

            }
            else if (wallright)
            {
                StartWallRun();
            }
            else
                StopWallRun();

        }

        if (isGrounded && isSprinting && !isCrouching && Input.GetAxisRaw("Vertical") > 0)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        if (isGrounded && !isSprinting && !isCrouching && Input.GetAxisRaw("Vertical") > 0)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);

        if (isGrounded && isCrouching && !isSliding && Input.GetAxisRaw("Vertical") > 0)
            animator.SetBool("CrouchWalk", true);
        else
            animator.SetBool("CrouchWalk", false);

        if (isGrounded &&  isCrouching && !isSliding)
            animator.SetBool("Crouched", true);
        else
            animator.SetBool("Crouched", false);

        if (!isGrounded)
            animator.SetBool("Jumping", true);
        else
            animator.SetBool("Jumping", false);

        if (isSliding)
            animator.SetBool("Sliding", true);
        else
            animator.SetBool("Sliding", false);


        if (isGrounded && isCrouching && rb.velocity.magnitude > slideMinSpeed)
        {
            isSliding = true;
            moveVert = 0;
            slideJump = slideJumpForce;
        }
        else
        {
            isSliding = false;
            moveVert = 1;
            slideJump = 0;
        }
    }

    void ControlSpeed()
    {
        if (isSprinting && !isCrouching)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Runfov, RunfovTime * Time.deltaTime);
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
            camTranslate = Mathf.Lerp(camTranslate, 0, camAcceleration * Time.deltaTime);

        }
        else if (isGrounded && !isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            camTranslate = Mathf.Lerp(camTranslate, 0, camAcceleration * Time.deltaTime);
        }
        else if (isGrounded && isCrouching)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
            if (isSliding)
                camTranslate = Mathf.Lerp(camTranslate, 1.4f, camAcceleration * Time.deltaTime);
            else
                camTranslate = Mathf.Lerp(camTranslate, 1, camAcceleration * Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, RunfovTime * Time.deltaTime);
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
            camTranslate = Mathf.Lerp(camTranslate, 0, camAcceleration * Time.deltaTime);
        }

    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement * moveVert + orientation.right * horizontalMovement * moveVert;
        moveAirDirection = orientation.right * horizontalMovement + (orientation.forward * verticalMovement) * 0.01f;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce + orientation.forward * slideJump * rb.velocity.magnitude, ForceMode.Impulse);
    }


    void ControlDrag()
    {
        if (isGrounded)
        {
            if (isSliding)
            {
                rb.drag = slideDrag;
            }
            else 
            {
                rb.drag = groundDrag;
            }
            rb.useGravity = true;
            StopWallRun();
        }
        else if (wallLeft | wallright)
        {
            rb.drag = wallDrag;
        }
        else
            rb.drag = airDrag;

    }

    private void FixedUpdate()
    {
        MovePlayer();

    }

    void MovePlayer()
    {
        if (isGrounded && !onSlope())
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Acceleration);
        else if (!isGrounded)
            rb.AddForce(moveAirDirection.normalized * moveSpeed * airMultiplier);
        else if (isGrounded && onSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed, ForceMode.Acceleration);
        }
    }


    /// 
    ///  Wall Running
    ///


    [Header("Wall Running")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    [SerializeField] private float wallrunGravity;
    [SerializeField] private float wallrunJumpForce;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float Runfov;
    [SerializeField] private float RunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;

    public float tilt { get; private set; }


    public bool wallLeft = false;
    public bool wallright = false;
    bool firstTouch = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    bool CanWallrun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight) && !isGrounded;
    }


    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallright = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    void StartWallRun()
    {
        if (wallLeft)
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        else if (wallright)
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);



        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, Runfov, RunfovTime * Time.deltaTime);

        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallrunGravity, ForceMode.Force);
        if (firstTouch == false)
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * 0.2f + 3, rb.velocity.z);
            firstTouch = true;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallrunJumpForce * 100, ForceMode.Force);
            }
            else if (wallright)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallrunJumpForce * 100, ForceMode.Force);
            }
        }
    }

    void StopWallRun()
    {
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
        rb.useGravity = true;
        firstTouch = false;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, RunfovTime * Time.deltaTime);
    }


}
