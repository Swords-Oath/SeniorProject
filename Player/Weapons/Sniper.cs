// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Sniper : MonoBehaviour
{
    // Weapon Values
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 30f;
    public float impactForce = 100f;
    public float mag = 15;

    // Player elements
    public GameObject Player;
    public Text AmmoText;
    public LayerMask mask;

    // Time related variables
    float time;
    bool Reloading = false;
    private float nextTimeToFire = 0f;

    // Weapon Effects
    public Camera fpsCam;
    public ParticleSystem shotFlash;
    public GameObject impactEffect;

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        // Check if player is connected to client
        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        // Set Ammo Text Box
        AmmoText.text = "Mag: " + mag.ToString();

        SetMag();

        if (Reloading)
        {
            time += Time.deltaTime;
            // Calls reload function
            Reload(15);
        }

        // When player left clicks call shoot function if mag is empty reload magazine
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && mag > 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (mag <= 0)
        {
            Reloading = true;
        }

    }

    public void SetMag()
    {
        // Sets weapon values
        var main = impactEffect.GetComponent<ParticleSystem>().main;
        main.startSpeed = 10;
        main.startSize = 0.6f;
        main.maxParticles = 100;
    }

    void Reload(float magsize)
    {
        if (time > 3)
        {
            // After reload time is reached reset mag variable
            mag = magsize;
            Reloading = false;
            time = 0;
        }
    }

    void Shoot()
    {
        // Plays fire animation
        shotFlash.Play();

        // Gets end point of the gun to the ground
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, mask))
        {
            // if ray hits an enemy will decrease enemy health
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // pushes back the enemy based on weapons push back
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // decreases total magazine and creates an explosion at point of impact
            mag -= 1;
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
