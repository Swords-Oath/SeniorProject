// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MissionPage : NetworkBehaviour
{
    // Variables
    GameObject Player;

    // Gets the MissionScreen
    public Camera MissionBoard;
    public bool MissionBoardShowing;

    private void OnTriggerEnter(Collider collision)
    {
        // If player Collides with game object this function is set to run this function
        if(collision.tag == "Player")
        {
            Player = GameObject.Find("Player");
            // Gets active player
            if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

            // Sets player values
            //Player.transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
            Player.transform.GetChild(7).gameObject.SetActive(true);
            Player.GetComponent<Weapons>().MainWeapon.SetActive(false);
            MissionBoardShowing = true;
        }
    }

    private void Update()
    {
        // Locks cursor if mission screen if showing
        if (MissionBoardShowing == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
