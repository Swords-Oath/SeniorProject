// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DestroyAfterTime : MonoBehaviour
{
    // Gets time existed
    public int LifeTime;
    float time = 1;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // Destroys gameobject once time is reached
        if(time >= LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
