// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Mirror;

public class Rifle : MonoBehaviour
{
    // Weapon Values & Player model
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 30f;
    public float impactForce = 100f;
    public GameObject Player;
    public Text AmmoText;
    public float mag = 60;
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
        // Gets if player is connected to active client
        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        // Set Ammo Text Box
        AmmoText.text = "Mag: " + mag.ToString();

        // Sets Weapon Values
        SetMag();

        if (Reloading)
        {
            time += Time.deltaTime;
            // Resets magazine
            Reload(60);
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && mag > 0)
        {
            // On left click lock cursor and call shoot function
            Cursor.lockState = CursorLockMode.Locked;
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        } else if (mag <= 0){ 
            // if magazine equals zero reload
            Reloading = true;
        }
        
    }

    public void SetMag()
    {

        // Sets Weapon Values to set amounts
        var main = impactEffect.GetComponent<ParticleSystem>().main;
        main.startSpeed = 15;
        main.startSize = 0.3f;
        main.maxParticles = 20;
    }

    void Reload(float magsize)
    {
        if (time > 1)
        {
            // Once reload time is met set magazine to original value and reset time
            mag = magsize;
            Reloading = false;
            time = 0;
        }
    }

    void Shoot()
    {
        // Play animation
        shotFlash.Play();

        // creates a ray from the weapon to the collision point based an range 
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, mask))
        {
            // if object has a health bar decrease it by damage value
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // push back object by impact force
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // decrease magazine by 1
            mag -= 1;

            // Create a particle effect and hit point of the ray
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
