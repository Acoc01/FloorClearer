using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 3f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float groundRayLength = 0.2f;
    public int airJumps = 1;

    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity;

    private float _defaultSpeed;
    private int _jumpsRemainig;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        _defaultSpeed = movementSpeed;
        _jumpsRemainig = airJumps;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        bool isWalking = Input.GetKey(KeyCode.LeftControl);
        
        Vector3 moveDirection = (cameraTransform.forward.normalized * verticalInput + cameraTransform.right.normalized * horizontalInput);
        moveDirection.y = 0f;

        if(isSprinting){
            movementSpeed = 2f*_defaultSpeed;
        }
        else if(isWalking){
            movementSpeed = 0.5f*_defaultSpeed;
        }
        else {
            movementSpeed = _defaultSpeed;
        }
        Vector3 moveVelocity = moveDirection.normalized * movementSpeed;

        ApplyGravity();
        
        controller.Move((moveVelocity + velocity) * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            if (velocity.y < 0)
            {
                velocity.y = -1f; // Prevent accumulating gravity when grounded
            }
        }
    }

    private void HandleRotation()
    {
        // Quaternion targetRotation = Quaternion.LookRotation(new Vector3(cameraTransform.forward.x, 0f, cameraTransform.right.z));
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(IsGrounded()){
                _jumpsRemainig = airJumps;
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
            else if(_jumpsRemainig > 0){
                _jumpsRemainig--;
                velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            }
            
        }
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position + controller.center;
        
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, groundRayLength))
        {
            return true;
        }

        return false;
    }
}
