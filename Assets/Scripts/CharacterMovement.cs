using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.5f;
    private float gravityValue = -9.81f;
    private CharacterController controller;
    private float walkSpeed = 10;
    private float runSpeed = 20;
    private float rotationSpeed = 180;
    private Vector3 rotation;
 
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        // Making sure we dont have a Y velocity if we are grounded
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Moving the character foward according to the speed
        Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
        move = this.transform.TransformDirection(move);
        if (Input.GetButton("Fire3"))// Left shift
        {
             controller.Move(move * runSpeed);
        }
        else
        {
            controller.Move(move * walkSpeed);
        }
        // Turning the character
        this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime, 0);
        this.transform.Rotate(this.rotation);

        // Jumping
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

         // Since there is no physics applied on character controller we have this applies to reapply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
