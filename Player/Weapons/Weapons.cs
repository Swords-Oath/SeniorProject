// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using Mirror;
using UnityEngine.SceneManagement;

public class Weapons : NetworkBehaviour
{
    // List of Available Weapons
    [SerializeField] public GameObject[] WeaponsList = new GameObject[4];

    // Player Rig
    public Rig DoubleHand;
    public Rig SingleHand;

    // Weapon Varaibles
    public string MainWeaponString = "";
    public GameObject MainWeapon;
    public GameObject mSelected;

    // UI GameObject
    public GameObject SniperZoom;

    // PlayerMovement Variable
    float PlayerMovement = 6;

    // Weapon Feature Variables
    float mag;
    float Mmag;
    GameObject Main;
    public bool WeaponOn;

    // Weapon Positioning Variables
    public Transform WeaponPosSingleHand;
    public Transform WeaponPosDoubleHand;
    Transform WeaponPos;

    private void Start()
    {
        // Gets Player Weapon
        MainWeaponString = GameObject.Find("PlayerPresets").GetComponent<SetPlayerItems>().Weapon;
        PickWeapon();

        // If Sniper activates Scope UI
        if (MainWeaponString != "Sniper")
        {
            SniperZoom.GetComponent<Scope>().scopeOverlay.SetActive(false);
            SniperZoom.SetActive(false);
        }

        // Modifies Movement Speed based on weapon
        MovementSpeed(MainWeaponString);
    }

    // Function to Get Players weapon
    void PickWeapon()
    {
        // Goes through all weapons to find the weapon chosen by the player and sets it as active
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

        // Sets all variables based on weapon chosen
        SetPos(MainWeaponString);
        Main = MainWeapon;
        GetMag(MainWeaponString, Main);
        Mmag = mag;
    }

    [ClientCallback]
    void Update()
    {
        // Makes sure player is the active client player
        if (!hasAuthority) { return; }

        // Sets player name so it isn't showing copy
        this.name = "Player";

        // Makes sureplayer isn't on the menu screen
        if (SceneManager.GetActiveScene().name == "Main")
        {
            GetMag(MainWeaponString, Main);
            Mmag = mag;

            SetPos(MainWeaponString);

            SetMag(MainWeaponString, Main, Mmag);
            WeaponOn = true;
        }
    }

    public void RemoveWeapon()
    {
        MainWeapon.SetActive(false);
    }

    public void EquipWeapon()
    {
        MainWeapon.SetActive(true);
    }

    void MovementSpeed(string weapon)
    {
        if (weapon == "Rifle")
        {
            PlayerMovement = 6;
        }
        else if (weapon == "Pistol")
        {
            PlayerMovement = 12;
        }
        else if (weapon == "Heavy")
        {
            PlayerMovement = 5;
        }
        else if (weapon == "Sniper")
        {
            PlayerMovement = 8;
        }

        GetComponentInParent<FPSMovement>().SetSpeed = PlayerMovement;
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
