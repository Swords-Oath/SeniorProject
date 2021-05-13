// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Pistol : MonoBehaviour
{
    // Weapon Values & Player model
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 30f;
    public float impactForce = 100f;
    public GameObject Player;
    public Text AmmoText;
    public float mag = 6;
    public LayerMask mask;

    // Weapon Time Values
    float time;
    bool Reloading = false;
    private float nextTimeToFire = 0f;

    // Particle Effects
    public Camera fpsCam;
    public ParticleSystem shotFlash;
    public GameObject impactEffect;

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        // Checks if player is connected to active client
        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        // Set Ammo Text Box
        AmmoText.text = "Mag: " + mag.ToString();

        // Sets Weapon Magazine
        SetMag();

        if (Reloading)
        {
            time += Time.deltaTime;
            // Resets weapon magazine
            Reload(6);
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && mag > 0)
        {
            // Locks cursor and calls shoot function when left click input
            Cursor.lockState = CursorLockMode.Locked;
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (mag <= 0)
        {
            // Reloads when magazine is 0
            Reloading = true;
        }

    }

    public void SetMag()
    {
        // Set Weapon variables
        var main = impactEffect.GetComponent<ParticleSystem>().main;
        main.startSpeed = 10;
        main.startSize = 0.6f;
        main.maxParticles = 50;
    }

    void Reload(float magsize)
    {
        // When reload time hits set mag to original value and resets time
        if (time > 1)
        {
            mag = magsize;
            Reloading = false;
            time = 0;
        }
    }

    void Shoot()
    {
        // Plays Animation
        shotFlash.Play();

        // Creates a ray from the start of the weapon to where it collides with an object as long as it's in range set
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, mask))
        {
            // If collided object has a health bar decrease it by damage variable
            EnemyHealth target = hit.transform.GetComponent <EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // push target back by impact force
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // decrease magazine by 1 and make particle effect at collision spot
            mag -= 1;
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
