using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using System;
public class Scope : MonoBehaviour
{
    // Animation of weapon and player model
    Animator animator;
    public GameObject Player;

    // Variable for active weapon and camera
    GameObject Camera;
    GameObject Weapon;
    GameObject WeaponRender;

    // UI for the scope
    public GameObject scopeOverlay;

    // Variable for if player is scoping
    bool Scoping = false;

    // Set mouse movement when scoping
    float ScopeSensitivity = 50f;
    float OriginalSensitivity = 250f;

    private void Start()
    {
        // Set UI to inactive
        scopeOverlay.SetActive(false);
    }

    [ClientCallback]
    void Update()
    {
        // Checks if player
        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        // Checks when player clicks to scope
        if (Input.GetButton("Fire2") && SceneManager.GetActiveScene().name != "Menu")
        {
            if (Scoping == false)
            {
                // Plays scope animation
                gameObject.GetComponent<Animator>().SetBool("Scoped", true);

                // Runs scope function
                StartCoroutine(Scoped());
            }

        }
        else 
        {
            if (Scoping == true)
            {
                // Stops playing scope animation
                gameObject.GetComponent<Animator>().SetBool("Scoped", false);

                // Reset Damage if you were scoping
                Weapon.GetComponent<Sniper>().damage -= 0.1f;

                // Stops scoping
                Unscoped();
            }

        }

        if (Scoping == false)
        {
            // Sets scope animation to false
            gameObject.GetComponent<Animator>().SetBool("Scoped", false);

            // Sets to not scoping
            Unscoped();
        }
    }

    void Unscoped()
    {
        // Sets Variables to the game objects in the scene
        Camera = Player.transform.GetChild(3).gameObject;
        Weapon = Player.GetComponent<Weapons>().MainWeapon;
        WeaponRender = Weapon.transform.GetChild(0).gameObject;
        // Set Scope Values to false and set values modified back to previous state
        Scoping = false;
        scopeOverlay.SetActive(false);
        WeaponRender.SetActive(true);
        Camera.GetComponent<Camera>().fieldOfView = 60f;
        Player.GetComponent<FPSView>().mouseSensitivity = OriginalSensitivity;

    }

    IEnumerator Scoped()
    {
        // Sets Scoping true 
        Scoping = true;
        // Sets Variables to the game objects in the scene
        Camera = Player.transform.GetChild(3).gameObject;
        Weapon = Player.GetComponent<Weapons>().MainWeapon;
        WeaponRender = Weapon.transform.GetChild(0).gameObject;
        // Waits specified amount of time
        yield return new WaitForSeconds(.20f);
        // Zooms in Camera, decreases sensitivity, sets weapon model to not show, and sets ui to active
        Camera.GetComponent<Camera>().fieldOfView = 25f;
        OriginalSensitivity = Player.GetComponent<FPSView>().mouseSensitivity;
        Player.GetComponent<FPSView>().mouseSensitivity = ScopeSensitivity;
        Weapon.GetComponent<Sniper>().damage += 0.1f;
        scopeOverlay.SetActive(true);
        WeaponRender.SetActive(false);        
    }
}
