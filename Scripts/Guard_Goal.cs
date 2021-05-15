// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Mirror;

public class Guard_Goal : NetworkBehaviour
{
    // Gets amount of enemies made it past
    [SyncVar]
    public int Points;
    public Text PointText;

    // Gets time spent in mission
    [SyncVar]
    float time;
    int timeRounded;
    public Text TimeText;

    // Gets Exit out of mission
    public GameObject Doors;
    public UnityEvent SwitchScene;

    private void Update()
    {
        // Sets text 
        time += Time.deltaTime;
        timeRounded = Mathf.RoundToInt(time);
        TimeText.text = "Time: " + timeRounded.ToString();

        PointText.text = "Break-Ins: " + Points.ToString();

        // If win or loss conditions met end mission
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
        // Increases enemies points
        if (other.tag == "EnemyPoint")
        {
            Points++;
            Destroy(other);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Increases enemies points
        Points++;
        Destroy(collision.gameObject);
    }
}
