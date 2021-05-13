using System.Collections;
// Programs
using System.Collections.Generic;
using UnityEngine;

public class RandomPathNPC : MonoBehaviour
{
    // x and z cordinates
    public int minX;
    public int maxX;
    public int minZ;
    public int maxZ;

    int x;
    int z;

    void OnTriggerEnter(Collider other)
    {
        // On Collision with an NPC set position to random range in min and max variables
        if (other.tag == "NPC")
        {
            x = Random.Range(minX, maxX);
            z = Random.Range(minZ, maxZ);

            //transforms position to new position
            this.gameObject.transform.position = new Vector3 (x, 10, z);


        }
    }
}
