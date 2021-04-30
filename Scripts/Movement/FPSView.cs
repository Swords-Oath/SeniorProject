using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class FPSView : NetworkBehaviour
{

    [SerializeField] public float mouseSensitivity = 100f;

    [SerializeField] public Transform playerBody;
    [SerializeField] public Transform Camera;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        if (!hasAuthority) { return; }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        Camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

 
    }

}
