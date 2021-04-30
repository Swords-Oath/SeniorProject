using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPathNPC : MonoBehaviour
{
    public int pivotPoint;

    public List<Vector3> paths = new List<Vector3>();

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {

            pivotPoint++;
            
            if (pivotPoint >= paths.Count)
            {
                pivotPoint = 0;
            }

            this.gameObject.transform.position = paths[pivotPoint];

            
        }
    }
}
