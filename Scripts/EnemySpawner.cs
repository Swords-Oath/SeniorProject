// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Variables for the spawn points of the enemies and the enemy models
    public List<Transform> Spawns = new List<Transform>();
    float PastSpawn;
    public GameObject Crawlers;
    float time;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // gets time and everytime a second passes creates an enemy at a random spawnpoint
        if(time >= 1f)
        {
            var SpawnNumber = Random.Range(0, 7);

            if (SpawnNumber != PastSpawn)
            {
                Instantiate(Crawlers, Spawns[SpawnNumber].position, Spawns[SpawnNumber].rotation);
                time = 0;
                PastSpawn = SpawnNumber;
            }
            

        }
    }
}
