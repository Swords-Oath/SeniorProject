using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health;
    public Image Healthbar;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Healthbar.fillAmount -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
