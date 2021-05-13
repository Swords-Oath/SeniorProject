using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour
{
    public GameObject WeaponImage;
    public string Weapon;
    public GameObject PlayerPresets;
    public GameObject Lobby;

    public GameObject[] Weapons = new GameObject[4];

    public void SetWeapon()
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }

        PlayerPresets.GetComponent<SetPlayerItems>().Weapon = Weapon;
        WeaponImage.SetActive(true);
    }

}
