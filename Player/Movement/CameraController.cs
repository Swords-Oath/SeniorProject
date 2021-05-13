// Program
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Disables Camera
        if (!this.transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            gameObject.GetComponent<Camera>().enabled = false;
            gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }
}
