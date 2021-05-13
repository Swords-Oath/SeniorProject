// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPathNPC : MonoBehaviour
{
    // List of positions for the npc to move between
    int pivotPoint;
    public List<Vector3> paths = new List<Vector3>();

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            // increases position number
            pivotPoint++;
            
            // if next point to move to isn't on the list reset
            if (pivotPoint >= paths.Count)
            {
                pivotPoint = 0;
            }

            // if npc collides with location then sets location to move to
            this.gameObject.transform.position = paths[pivotPoint];

            
        }
    }
}
