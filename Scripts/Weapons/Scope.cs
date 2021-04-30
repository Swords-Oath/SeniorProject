using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Scope : MonoBehaviour
{
    Animator animator;
    public GameObject Player;

    GameObject Weapon;
    public GameObject scopeOverlay;

    [ClientCallback]
    void Update()
    {
        if (!Player.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        if (Input.GetButton("Fire2"))
        {
            gameObject.GetComponent<Animator>().SetBool("Scoped", true);

            StartCoroutine(Scoped());
        }
        else 
        {
            gameObject.GetComponent<Animator>().SetBool("Scoped", false);

            Unscoped();
        }
    }

    void Unscoped()
    {
        scopeOverlay.SetActive(false);
    }

    IEnumerator Scoped()
    {
        yield return new WaitForSeconds(.20f);
        scopeOverlay.SetActive(true);
    }
}
