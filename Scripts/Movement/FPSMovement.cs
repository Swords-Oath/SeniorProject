using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FPSMovement : NetworkBehaviour
{
    [SerializeField] public CharacterController controller;
    [SerializeField] public Animator WalkAni;

    [SerializeField] public float speed = 12;
    [SerializeField] public float gravity = -9.81f;
    [SerializeField] public float jumpHeight = 3f;

    [SerializeField] public Transform groundCheck;
    [SerializeField] public float groundDistance = 0.4f;
    [SerializeField] public LayerMask groundMask;

    [SerializeField] Vector3 velocity;
    [SerializeField] bool isGrounded;
    [SerializeField] float SetSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed = speed;
    }

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        if (!hasAuthority) { return; }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed + 5;
            WalkAni.speed += 1;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = SetSpeed;
            WalkAni.speed -= 1;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
            WalkAni.SetBool("IsWalking", true);
        } else {
            WalkAni.SetBool("IsWalking", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            
        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
