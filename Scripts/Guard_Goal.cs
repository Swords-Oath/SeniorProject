using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Guard_Goal : MonoBehaviour
{
    public int Points;
    public Text PointText;

    float time;
    int timeRounded;
    public Text TimeText;

    public GameObject Doors;
    public UnityEvent SwitchScene;

    private void Update()
    {
        time += Time.deltaTime;
        timeRounded = Mathf.RoundToInt(time);
        TimeText.text = "Time: " + timeRounded.ToString();

        PointText.text = "Break-Ins: " + Points.ToString();

        if (Points >= 50)
        {
            SwitchScene.Invoke();
        }

        if (timeRounded >= 250)
        {
            Destroy(Doors);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyPoint")
        {
            Points++;
            Destroy(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Points++;
        Destroy(collision.gameObject);
    }
}
