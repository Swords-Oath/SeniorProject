// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawn : MonoBehaviour
{
    // Gets spawnpoint
    public Transform SpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        // Moves player to spawn when active
        SpawnPoint.position = gameObject.transform.position;
    }
}
