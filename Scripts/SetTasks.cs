// Programs
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SetTasks : MonoBehaviour
{
    // Gets text of the player task
    public Text Task;

    // Update is called once per frame
    void Start()
    {
        // Sets it based on the scene they are in
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Task.text = "Current Task: Go to the mission terminal and start a mission.";
        }
        else if (SceneManager.GetActiveScene().name == "Stealth")
        {
            Task.text = "Current Task: Make your way to the other side of the base without being detected.";
        }
        else if (SceneManager.GetActiveScene().name == "Guard")
        {
            Task.text = "Current Task: Survive for 250 seconds and stop enemies from entering the camp.";
        }
    }
}
