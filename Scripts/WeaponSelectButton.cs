using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    public string Weapon;
    GameObject Player;
    public GameObject Lobby;

    private void Update()
    {
        if (Lobby.activeSelf == true)
        {
            Player = GameObject.Find("Player");
        }
    }

    public void SetWeapon()
    {
        Player.GetComponent<Weapons>().MainWeaponString = Weapon;
    }

}
