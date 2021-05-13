using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpyMissionEnemies : MonoBehaviour
{
    public UnityEvent decreaseHP;
    public Transform SpawnPoint;
    GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Player = GameObject.Find("Player");
            Player.GetComponent<Healthbar>().health -= 1f;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3);
        decreaseHP.Invoke(); 

    }
}
