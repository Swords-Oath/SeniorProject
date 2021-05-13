// Program
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ClassSelectButton : MonoBehaviour
{
    // Variables for Classes screen, lobby screen, and player values
    public GameObject ClassImage;
    public string ClassName;
    public GameObject PlayerPresets;
    public GameObject Lobby;
    public GameObject[] Classes = new GameObject[3];

    // When run will set the player value class to the class selected
    public void SetClass()
    {
        // runs through available classes and gets the one chosen
        for (int i = 0; i < Classes.Length; i++)
        {
            Classes[i].SetActive(false);
        }

        // Sets the class chosen to player value
        PlayerPresets.GetComponent<SetPlayerItems>().Class = ClassName;
        ClassImage.SetActive(true);
    }
}
