using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class Weapons : NetworkBehaviour
{
    public GameObject[] WeaponsList = new GameObject[4];
    public Rig DoubleHand;
    public Rig SingleHand;

    public string MainWeaponString = "";
    public GameObject MainWeapon;
    public GameObject mSelected;

    public GameObject WeaponZoom;

    float mag;
    float Mmag;
    GameObject Main;
    public bool WeaponOn;

    public Transform WeaponPosSingleHand;
    public Transform WeaponPosDoubleHand;
    Transform WeaponPos;

    // Update is called once per frame
    void PickWeapon()
    {
        
        for (int i = 0; i < WeaponsList.Length; i++)
        {
            if(WeaponsList[i].name == MainWeaponString)
            {
                WeaponsList[i].SetActive(true);
                MainWeapon = WeaponsList[i];
            }
            else
            {
                WeaponsList[i].SetActive(false);
            }
        }

        SetPos(MainWeaponString);
        Main = MainWeapon;
        GetMag(MainWeaponString, Main);
        Mmag = mag;
    }

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority) { return; }

        if (SceneManager.GetActiveScene().name == "Main")
        {
            PickWeapon();

            GetMag(MainWeaponString, Main);
            Mmag = mag;

            SetPos(MainWeaponString);

            SetMag(MainWeaponString, Main, Mmag);
            WeaponOn = true;
        }
    }

    void SetPos(string weapon)
    {
        if (weapon == "Rifle")
        {
            WeaponPos = WeaponPosDoubleHand;
            SingleHand.weight = 0;
            DoubleHand.weight = 1;
        }
        else if (weapon == "Pistol")
        {
            WeaponPos = WeaponPosSingleHand;
            SingleHand.weight = 1;
            DoubleHand.weight = 0;
        }
        else if (weapon == "Heavy")
        {
            WeaponPos = WeaponPosDoubleHand;
            SingleHand.weight = 0;
            DoubleHand.weight = 1;
        }
        else if (weapon == "Sniper")
        {
            WeaponPos = WeaponPosDoubleHand;
            SingleHand.weight = 0;
            DoubleHand.weight = 1;
        }
    }

    void GetMag(string weapon, GameObject Set)
    {
        if (weapon == "Rifle")
        {
            mag = Set.GetComponent<Rifle>().mag;
        } else if (weapon == "Pistol")
        {
            mag = Set.GetComponent<Pistol>().mag;
        } else if (weapon == "Heavy")
        {
            mag = Set.GetComponent<Heavy>().mag;
        }
        else if (weapon == "Sniper")
        {
            mag = Set.GetComponent<Sniper>().mag;
        }
    }

    void SetMag(string weapon, GameObject Set, float magazine)
    {
        if (weapon == "Rifle")
        {
            Set.GetComponent<Rifle>().mag = magazine;
        }
        else if (weapon == "Pistol")
        {
            Set.GetComponent<Pistol>().mag = magazine;
        }
        else if (weapon == "Heavy")
        {
            mag = Set.GetComponent<Heavy>().mag;
        }
        else if (weapon == "Sniper")
        {
            mag = Set.GetComponent<Sniper>().mag;
        }
    }
}
