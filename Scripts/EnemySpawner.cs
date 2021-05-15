// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
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
                CmdSpawnEnemy(SpawnNumber);
                time = 0;
                PastSpawn = SpawnNumber;
            }
            

        }
    }

    void CmdSpawnEnemy(int SpawnNumber)
    {
        var Crawler = (GameObject)Instantiate(Crawlers, Spawns[SpawnNumber].position, Spawns[SpawnNumber].rotation);
        NetworkServer.Spawn(Crawler);

    }
}
