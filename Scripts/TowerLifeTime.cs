using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TowerLifeTime : NetworkBehaviour
{
    // Life Time of the tower
    [SyncVar]
    float timeAlive;

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        // if tower is present longer then alotted time will reset values and delete the object
        if (timeAlive >= 25)
        {
            if (GameObject.Find("Player").GetComponent<TowerSkills>().TowerActive == "AttackTower")
            {
                GameObject.Find("Player").GetComponent<TowerSkills>().DecreaseDmg();
                GameObject.Find("Player").GetComponent<TowerSkills>().TowerActive = "";
            }

            
            CmdDestroyTower();
        }
    }

    void CmdDestroyTower()
    {
        // Destorys tower
        Destroy(gameObject);
    }
}
