using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquidCollision : MonoBehaviour
{
    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;
    public int Health = 3;

    public UnityEvent noHealth;

    public void SquidCollider()
    {


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
        Health--;

    }
}
