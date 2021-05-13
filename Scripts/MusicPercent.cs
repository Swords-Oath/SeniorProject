// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MusicPercent : MonoBehaviour
{
    // Variables for Options panel
    public GameObject Slider;
    public bool MorS;
    AudioSource Music;
    private float Percent;

    private void Start()
    {
        // Sets Variables of the gameobject it is to represent.
        if (MorS == true)
        {
            Music = GameObject.Find("Music").GetComponent<AudioSource>();
        }
        else if (MorS == false)
        {
            Music = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        }

    }
    // Update is called once per frame
    void Update()
    {
        // Sets Option Panel Values
        Percent = Slider.GetComponent<Slider>().value;
        Music.volume = Percent;
        Percent = Mathf.Round(Percent * 100.0f) * 1f;
        gameObject.GetComponent<TMP_Text>().text = Percent.ToString() + "%";
    }
}
