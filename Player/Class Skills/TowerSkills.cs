// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerSkills : MonoBehaviour
{
    // Weapon Variables
    GameObject Weapon;
    string WeaponName;
    bool Damage;

    // Type of Tower
    public string TowerActive;
    bool Heal = false;

    // Time Variables
    float time;
    float timeAlive;
    

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            // Gets Type of tower and preforms the ability attached to the tower
            if (other.tag == "AttackTower")
            {
                Weapon = gameObject.GetComponent<Weapons>().MainWeapon;
                WeaponName = Weapon.name;
                BuffDmg();
                TowerActive = "Attack";
            }
            else if (other.tag == "SupportTower")
            {
                Heal = true;
                TowerActive = "Support";
            }

        } catch (NullReferenceException)
        {

        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        // Gets time and heals players in the area when time is reached
        if (time >= 10 & Heal == true)
        {
            try
            {
                gameObject.GetComponent<Healthbar>().health += 0.1f;
                time = 0;
            }
            catch (NullReferenceException)
            {

            }
        
        }

    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            // Resets tower values when player leaves the effect range
            if (other.tag == "AttackTower")
            {
                Weapon = gameObject.GetComponent<Weapons>().MainWeapon;
                WeaponName = Weapon.name;
                DecreaseDmg();
                TowerActive = "";
            }
            else if (other.tag == "SupportTower")
            {
                Heal = false;
                TowerActive = "";
            }

        }
        catch (NullReferenceException)
        {

        }
    }

    void BuffDmg()
    {
        // gets player weapon and increases the dmg it deals
        if (WeaponName == "Pistol")
        {
            Weapon.GetComponent<Pistol>().damage += 0.01f;
        }
        else if(WeaponName == "Rifle")
        {
            Weapon.GetComponent<Rifle>().damage += 0.01f;
        }
        else if (WeaponName == "Heavy")
        {
            Weapon.GetComponent<Heavy>().damage += 0.01f;
        }
        else if (WeaponName == "Sniper")
        {
            Weapon.GetComponent<Sniper>().damage += 0.01f;
        }
    }

    public void DecreaseDmg()
    {
        // gets player weapon and decreases the dmg it deals
        if (WeaponName == "Pistol")
        {
            Weapon.GetComponent<Pistol>().damage -= 0.01f;
        }
        else if (WeaponName == "Rifle")
        {
            Weapon.GetComponent<Rifle>().damage -= 0.01f;
        }
        else if (WeaponName == "Heavy")
        {
            Weapon.GetComponent<Heavy>().damage -= 0.01f;
        }
        else if (WeaponName == "Sniper")
        {
            Weapon.GetComponent<Sniper>().damage -= 0.01f;
        }
    }
}
