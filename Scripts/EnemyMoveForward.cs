using System.Collections;
using System;
using UnityEngine;

public class EnemyMoveForward : MonoBehaviour
{
    public GameObject Explosion;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 3 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            try
            {
                collision.gameObject.GetComponent<Healthbar>().health -= 0.1f;
            }
            catch (NullReferenceException)
            {

            }
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "GuardTower")
        {
            Destroy(gameObject);
        }
    }
}
