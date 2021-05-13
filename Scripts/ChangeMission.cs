//Programs in use
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class ChangeMission : MonoBehaviour
{
    // Variables
    public GameObject Player;

    // ChangeScene Function
    public void ChangeToScene(string Mission)
    {
        // Gets player
        Player = GameObject.Find("Player");

        // Sets Player values
        Player.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
        Player.transform.GetChild(7).gameObject.SetActive(false);
        Player.GetComponent<FPSMovement>().enabled = true;
        Player.GetComponent<FPSView>().enabled = true;

        // Changes the Scene and Calls the Start Game Function to re-create player models 
        ((MyNetworkManager)NetworkManager.singleton).ChangeNextScene(Mission);
        ((MyNetworkManager)NetworkManager.singleton).StartGame();
    }
}
