// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpyMissionEnemies : MonoBehaviour
{
    // Player positions and event to reset player
    public UnityEvent decreaseHP;
    public Transform SpawnPoint;
    GameObject Player;

    void OnTriggerEnter(Collider other)
    {
        // if player collides with the enemie sends to spawn
        if (other.tag == "Player")
        {
            Player = GameObject.Find("Player");
            Player.GetComponent<Healthbar>().health -= 1f;
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        // Waits then allows player to move
        yield return new WaitForSeconds(3);
        decreaseHP.Invoke(); 

    }
}
