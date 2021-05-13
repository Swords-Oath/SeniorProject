// Program
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FPSView : NetworkBehaviour
{
    // View Sensitivity
    [SerializeField] public float mouseSensitivity = 100f;

    // Player model and camera
    [SerializeField] public Transform playerBody;
    [SerializeField] public Transform Camera;

    [SerializeField] public GameObject EscapeMenu;

    // Camera x Rotation
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        // Checks if player is current client
        if (!hasAuthority) { return; }

        // Sets rotational values 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Sets border for max Rotation of the Camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        // Rotates Camera and player model based on Cursor
        Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // Unlocks cursor when escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<Weapons>().RemoveWeapon();
            EscapeMenu.SetActive(true);
        }

 
    }

}
