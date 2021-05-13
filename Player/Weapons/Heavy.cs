// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class Heavy : MonoBehaviour
{
    // Gun Values
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 30f;
    public float impactForce = 100f;
    public GameObject Player;
    public Text AmmoText;
    public float mag = 120;
    public LayerMask mask;

    // Time managment variables
    float time;
    bool Reloading = false;
    private float nextTimeToFire = 0f;

    // Gets the GameObjects related to the gun
    public Camera fpsCam;
    public ParticleSystem shotFlash;
    public GameObject impactEffect;


    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        // gets active player
        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        // Set Ammo Text Box
        AmmoText.text = "Mag: " + mag.ToString();

        // Calls Function to set players magazine
        SetMag();

        if (Reloading)
        {
            time += Time.deltaTime;
            // Calls function to reload magazine
            Reload(120);
        }

        // Fires weapon when clicked if mag isn't empty
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && mag > 0)
        {
            // Locks cursor and calls the shoot function
            Cursor.lockState = CursorLockMode.Locked;
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (mag <= 0)
        {
            // if mag is empty reloads the magazine
            Reloading = true;
        }

    }

    public void SetMag()
    {
        //GameObject.Find("MainMagCount").GetComponent<Text>().text = mag.ToString();
        var main = impactEffect.GetComponent<ParticleSystem>().main;
        main.startSpeed = 20;
        main.startSize = 0.3f;
        main.maxParticles = 10;
    }

    void Reload(float magsize)
    {
        // once amount of time needed to reload is reached reload the gun
        if (time > 2)
        {
            // Set and reset needed values
            mag = magsize;
            Reloading = false;
            time = 0;
        }
    }

    void Shoot()
    {
        // Plays animation
        shotFlash.Play();

        // Gets point at the end of the weapon where the bullet would hit & if out of range don't shoot
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, mask))
        {
            // If shooting object with a health bar decrease the total health
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                // Calls function to decrease health of enemy
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                // Applies pushback to enemy based on force of impact of the weapon
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // Decreases magazine by 1
            mag -= 1;

            // Creates a particle effect at the point of impact of the bullet
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
