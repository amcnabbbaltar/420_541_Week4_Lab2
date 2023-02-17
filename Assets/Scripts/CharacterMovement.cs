using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Vector3 gravity;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    private float jumpHeight = 1f;
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private float walkSpeed = 5;
    private float runSpeed = 8; 
 
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
       ProcessMovement();
       UpdateAnimator();
       
    }
    void UpdateAnimator()
    {
        // TODO add animation SetParameters for Idle/Walk/Run
    }

    void ProcessMovement()
    { 
        // Moving the character foward according to the speed
        float speed = GetMovementSpeed();

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // Turning the character
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        // Making sure we dont have a Y velocity if we are grounded
        // controller.isGrounded tells you if a character is grounded ( IE Touches the ground)
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer)
        {
            if (Input.GetButtonDown("Jump") )
            {
                gravity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
            else 
            {
                // Dont apply gravity if grounded and not jumping
                gravity.y = -1.0f;
            }
        }
        else 
        {
            // Since there is no physics applied on character controller we have this applies to reapply gravity
            gravity.y += gravityValue * Time.deltaTime;
        }  
        playerVelocity = gravity * Time.deltaTime + move * Time.deltaTime * speed;
        controller.Move(playerVelocity);
    }

    float GetMovementSpeed()
    {
        if (Input.GetButton("Fire3"))// Left shift
        {
            return runSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }
}
