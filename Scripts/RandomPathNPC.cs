using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPathNPC : MonoBehaviour
{
    public int x;
    public int z;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            x = Random.Range(-83, -120);
            z = Random.Range(-72, -150);

            this.gameObject.transform.position = new Vector3 (x, 10, z);


        }
    }
}
