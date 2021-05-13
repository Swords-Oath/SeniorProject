using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLifeTime : MonoBehaviour
{
    float timeAlive;
    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive >= 25)
        {
            if (GameObject.Find("Player").GetComponent<TowerSkills>().TowerActive == "AttackTower")
            {
                GameObject.Find("Player").GetComponent<TowerSkills>().DecreaseDmg();
                GameObject.Find("Player").GetComponent<TowerSkills>().TowerActive = "";
            }

            Destroy(gameObject);
        }
    }
}
