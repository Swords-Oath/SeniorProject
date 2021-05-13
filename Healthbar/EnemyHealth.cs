using System.Collections;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    float health = 0.1f;
    public Animator EnemyAni;

    public void Update()
    {
        if (health <= 0f)
        {
            StartCoroutine(Die());
            
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    IEnumerator Die()
    {
        try
        {
            gameObject.GetComponent<EnemyMoveForward>().enabled = false;
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            
        } catch (NullReferenceException)
        {

        }

        EnemyAni.SetBool("Move Forward", false);
        EnemyAni.SetTrigger("Die");
        yield return new WaitForSeconds(2);
        Destroy(gameObject);

    }
}

