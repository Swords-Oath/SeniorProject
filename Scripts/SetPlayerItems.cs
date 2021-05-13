// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerItems : MonoBehaviour
{
    // Player Weapon and Class Variables
    public string Class;
    public string Weapon;

    private void Start()
    {
        // Stop from destroying when changing scenes
        DontDestroyOnLoad(gameObject);
    }

}
