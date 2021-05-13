using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public int LifeTime;
    float time = 1;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time >= LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
