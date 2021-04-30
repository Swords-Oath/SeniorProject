using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Pistol : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 30f;
    public float impactForce = 100f;
    public GameObject Player;

    public float mag = 6;
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

        SetMag();

        if (Reloading)
        {
            time += Time.deltaTime;
            Reload(6);
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && mag > 0)
        {
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
        if (Player.GetComponent<Weapons>().WeaponOn)
        {
            GameObject.Find("SubMagCount").GetComponent<Text>().text = mag.ToString();
        }
        else
        {
            GameObject.Find("MainMagCount").GetComponent<Text>().text = mag.ToString();
        }
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
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent <Target>();
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
