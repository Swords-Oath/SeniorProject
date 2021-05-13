using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Classes : NetworkBehaviour
{
    // Variable for player class
    public string Class;

    // Variables for the ability towers of each class
    GameObject Tower;
    public GameObject AttackTower;
    public GameObject DefenceTower;
    public GameObject SupportTower;

    private void Start()
    {
        // Gets player class
        Class = GameObject.Find("PlayerPresets").GetComponent<SetPlayerItems>().Class;
    }

    void Update()
    {
        // Gets player connected to the client
        if (!hasAuthority) { return; }

        // Gets player input
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Destorys previous tower
            Destroy(Tower);

            // Based on class spawns a new tower at player position
            if (Class == "Striker")
            {
                Tower = Instantiate(AttackTower, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                Tower.name = "StrikeTower";
            } else if(Class == "Guard")
            {
                Tower = Instantiate(DefenceTower, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                Tower.name = "GuardTower";
            } else if (Class == "Support")
            {
                Tower = Instantiate(SupportTower, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
                Tower.name = "SupportTower";
            }
        }
    }
}
