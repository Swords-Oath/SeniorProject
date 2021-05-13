using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FPSMovement : NetworkBehaviour
{
    // Movement Animation
    [SerializeField] public CharacterController controller;
    [SerializeField] public Animator WalkAni;

    // Movement Variables
    [SerializeField] public float speed = 12;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 3f;
    [SerializeField] Vector3 velocity;
    [SerializeField] public float SetSpeed = 6;
    [SerializeField] public bool Jumping = true;

    // Ground Check Variables
    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;
    [SerializeField] bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // Set speed to origin speed
        speed = SetSpeed;
    }

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        // Checks active player
        if (!hasAuthority) { return; }

        // Increases Speed when shift is down
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed + 5;
            WalkAni.speed += 1;
        }

        // Reverts Speed to Original speed when shift is up
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = SetSpeed;
            WalkAni.speed -= 1;
        }

        // Checks if player is on the ground 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Sets x and z value based on Player Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Moves Player based on new Variable values
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Sets player to Walking when player moves forward or backward
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            WalkAni.SetBool("IsWalking", true);
        } else {
            WalkAni.SetBool("IsWalking", false);
        }

        // Moves player model up when space bar is pressed
        if (Input.GetButtonDown("Jump") && isGrounded && Jumping == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
