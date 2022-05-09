using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Wallrun : NetworkBehaviour
{
    [SerializeField] Transform orientation;

    [Header("Wall Running")]
    [SerializeField] float wallDistance = .5f;
    [SerializeField] float minimumJumpHeight = 1.5f;

    [SerializeField] private float wallrunGravity;
    [SerializeField] private float wallrunJumpForce;

    bool wallLeft = false;
    bool wallright = false;
    bool firstTouch = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;
    
    private Rigidbody rb;

    bool CanWallrun()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit,wallDistance);
        wallright = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }

    private void Update()
    {
        CheckWall();

        if (CanWallrun())
        {
            if (wallLeft)
            {
                StartWallRun();
                Debug.Log("wallrun left");

            }
            else if (wallright)
            {
                StartWallRun();
                Debug.Log("wallrun right");
            }
            else
                StopWallRun();
        }
    }
    void StartWallRun()
    {
        rb.useGravity = false;
        rb.AddForce(Vector3.down * wallrunGravity, ForceMode.Force);
        if (firstTouch == false) 
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y *0.2f+3, rb.velocity.z);
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
        rb.useGravity = true;
        firstTouch = false;
    }

}
