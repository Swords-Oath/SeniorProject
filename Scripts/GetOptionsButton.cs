using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOptionsButton : MonoBehaviour
{
    public void GetOptions()
    {
        GameObject.Find("SavedOptions").GetComponent<SavedOptions>().PauseSettingSaves();
    }
}
