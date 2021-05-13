using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Sniper : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 30f;
    public float impactForce = 100f;
    public GameObject Player;
    public Text AmmoText;
    public LayerMask mask;

    public float mag = 15;
    float time;
    bool Reloading = false;

    public Camera fpsCam;
    public ParticleSystem shotFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    [ClientCallback]
    void Update()
    {
        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        // Set Ammo Text Box
        AmmoText.text = "Mag: " + mag.ToString();

        SetMag();

        if (Reloading)
        {
            time += Time.deltaTime;
            Reload(15);
        }

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
        //GameObject.Find("MainMagCount").GetComponent<Text>().text = mag.ToString();
        var main = impactEffect.GetComponent<ParticleSystem>().main;
        main.startSpeed = 10;
        main.startSize = 0.6f;
        main.maxParticles = 100;
    }

    void Reload(float magsize)
    {
        if (time > 3)
        {
            mag = magsize;
            Reloading = false;
            time = 0;
        }
    }

    void Shoot()
    {
        shotFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, mask))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            mag -= 1;
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
