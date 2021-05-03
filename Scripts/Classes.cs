using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Classes : NetworkBehaviour
{
    [SerializeField] public string Class;

    [SerializeField] public GameObject AttackTower;
    [SerializeField] public GameObject DefenceTower;
    [SerializeField] public GameObject SupportTower;

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority) { return; }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Class == "Striker")
            {
                Instantiate(AttackTower, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), Quaternion.identity);
                
            } else if(Class == "Guard")
            {
                Instantiate(DefenceTower, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), Quaternion.identity);
            } else if (Class == "Support")
            {
                Instantiate(SupportTower, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 5, gameObject.transform.position.z), Quaternion.identity);
            }
        }
    }
}
