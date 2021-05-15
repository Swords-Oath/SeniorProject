// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewPreset : MonoBehaviour
{
    // Gets the Button attached to
    public Button yourButton;

    void Start()
    {
        // Sets button object
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        // Detroys players settings
        Destroy(GameObject.Find("PlayerPresets")); 
    }
}
