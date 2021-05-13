using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public float health;
    public Image HealthImage;
    public GameObject SpawnPoint;

    private void Start()
    {
        SpawnPoint = GameObject.Find("SpawnPoint");
    }

    public void Update()
    {
        HealthImage.fillAmount = health;
        
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

        gameObject.transform.position = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y + 2, SpawnPoint.transform.position.z);

        gameObject.GetComponent<FPSMovement>().enabled = true;
        gameObject.GetComponent<FPSView>().enabled = true;

        yield return new WaitForSeconds(1);
        health = 1;

    }
}
