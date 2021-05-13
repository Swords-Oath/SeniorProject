// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetJumping : MonoBehaviour
{
    public bool Jump = false;

    void Update()
    {
        // Gets player and sets so they can't jump
        GameObject.Find("Player").GetComponent<FPSMovement>().Jumping = Jump;
    }
}
