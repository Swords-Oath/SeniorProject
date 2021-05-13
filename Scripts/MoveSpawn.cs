using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpawn : MonoBehaviour
{
    public Transform SpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        SpawnPoint.position = gameObject.transform.position;
    }
}
