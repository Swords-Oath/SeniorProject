// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Gets player position
    public Transform player;

    private void LateUpdate()
    {
        // Sets map to player location
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

    }

    void Update()
    {
        // Stops map from rotating with the player
        this.transform.rotation = Quaternion.Euler(90f, 0.0f, this.transform.parent.rotation.z * -1.0f);
    }
}
