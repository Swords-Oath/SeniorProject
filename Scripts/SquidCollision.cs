// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquidCollision : MonoBehaviour
{
    // Mission Health
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;
    public int Health = 3;

    // Event for when Mission ends
    public UnityEvent noHealth;

    public void SquidCollider()
    {

        // Gets Health and sets visuals based on total health
        if (Health == 3)
        {
            Health3.SetActive(false);
        }
        else if (Health == 2)
        {
            Health2.SetActive(false);
        }
        else if (Health == 1)
        {
            Health1.SetActive(false);
        }
        else
        {
            noHealth.Invoke();
        }

        // Decreases health value
        Health--;

    }
}
