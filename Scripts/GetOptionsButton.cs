// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOptionsButton : MonoBehaviour
{
    public void GetOptions()
    {
        // Gets player options for sound
        GameObject.Find("SavedOptions").GetComponent<SavedOptions>().PauseSettingSaves();
    }
}
