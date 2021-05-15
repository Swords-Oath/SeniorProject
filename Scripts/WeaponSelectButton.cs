// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    // Variables for Weapon UI and Player
    public GameObject WeaponImage;
    public string Weapon;
    public GameObject PlayerPresets;
    public GameObject Lobby;

    // Gets all models for the possible weapons
    public GameObject[] Weapons = new GameObject[4];

    public void SetWeapon()
    {
        // Sets the active model based on chosen weapon
        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }

        // Sets player weapon and set Lobby UI
        PlayerPresets.GetComponent<SetPlayerItems>().Weapon = Weapon;
        WeaponImage.SetActive(true);
    }

}
