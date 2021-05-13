// Programs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Mirror;

public class SavedOptions : MonoBehaviour
{
    // Bools for What options to set
    bool OptionsSet = true;
    bool SettingOptions = false;
    bool InitialSetting = false;

    // Options
    float SoundSlider = 1f;
    float MusicSlider = 1f;
    bool AudioEnabled = true;
    bool MusicEnabled = true;
    bool SoundEnabled = true;
    GameObject SoundEffects;
    GameObject Music;
    GameObject Audio;

    // Start is called before the first frame update
    void Start()
    {
        // Adds to a list to destroy all but the original options panel
        DontDestroyOnLoad(gameObject);
        GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>().SetOptions.Add(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checks the Scene
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            // trys if fails will not show an error
            try
            {
                // Sets Option Variables from Game Objects 
                Music = GameObject.Find("Music");
                SoundEffects = GameObject.Find("SoundEffects");
                Audio = GameObject.Find("Audio");

                SoundEffects.GetComponent<AudioSource>().volume = SoundSlider;
                Music.GetComponent<AudioSource>().volume = MusicSlider;

                Music.SetActive(MusicEnabled);
                SoundEffects.SetActive(SoundEnabled);
                Audio.SetActive(AudioEnabled);



            }
            catch (NullReferenceException)
            {
                // Catches if it can't find one of the Game Objects
            }
        }

        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            // Finds out if the options have already been set
            if (OptionsSet == true)
            {
                try
                {
                    // If first time it is called runs this program
                    if (InitialSetting == true)
                    {
                        // Sets the Game Objects to the variables
                        GameObject.Find("SoundEffectsVolume").GetComponent<Slider>().value = SoundSlider;
                        GameObject.Find("MusicVolume").GetComponent<Slider>().value = MusicSlider;


                        GameObject.Find("MusicTrigger").GetComponent<Toggle>().isOn = MusicEnabled;
                        GameObject.Find("SoundEffectsTrigger").GetComponent<Toggle>().isOn = SoundEnabled;
                        GameObject.Find("AudioTrigger").GetComponent<Toggle>().isOn = AudioEnabled;

                        InitialSetting = false;
                    }

                    // Continues the process of setting the options
                    Music = GameObject.Find("Music");
                    SoundEffects = GameObject.Find("SoundEffects");
                    Audio = GameObject.Find("Audio");

                    GameObject.Find("SoundEffectsVolume").GetComponent<Slider>().value = SoundSlider;
                    GameObject.Find("MusicVolume").GetComponent<Slider>().value = MusicSlider;


                    GameObject.Find("MusicTrigger").GetComponent<Toggle>().isOn = MusicEnabled;
                    GameObject.Find("SoundEffectsTrigger").GetComponent<Toggle>().isOn = SoundEnabled;
                    GameObject.Find("AudioTrigger").GetComponent<Toggle>().isOn = AudioEnabled;

                    Music.GetComponent<AudioSource>().volume = MusicSlider;
                    SoundEffects.GetComponent<AudioSource>().volume = SoundSlider;
                }
                catch (NullReferenceException)
                {
                    // Checks if any Game Objects isn't found
                }

                // Enables or Disables the Options based on the Variables set
                Music.SetActive(MusicEnabled);
                SoundEffects.SetActive(SoundEnabled);
                Audio.SetActive(AudioEnabled);

            }

            // If player is on the options panel allows the player to set the options.
            if (SettingOptions == true)
            {
                // If first time this function is called.
                if (InitialSetting == true)
                {
                    // Sets GameObjects to the options variables
                    GameObject.Find("SoundEffectsVolume").GetComponent<Slider>().value = SoundSlider;
                    GameObject.Find("MusicVolume").GetComponent<Slider>().value = MusicSlider;


                    GameObject.Find("MusicTrigger").GetComponent<Toggle>().isOn = MusicEnabled;
                    GameObject.Find("SoundEffectsTrigger").GetComponent<Toggle>().isOn = SoundEnabled;
                    GameObject.Find("AudioTrigger").GetComponent<Toggle>().isOn = AudioEnabled;

                    InitialSetting = false;
                }

                // Sets the variables to the GameObjects
                SoundSlider = GameObject.Find("SoundEffectsVolume").GetComponent<Slider>().value;
                MusicSlider = GameObject.Find("MusicVolume").GetComponent<Slider>().value;


                MusicEnabled = GameObject.Find("MusicTrigger").GetComponent<Toggle>().isOn;
                SoundEnabled = GameObject.Find("SoundEffectsTrigger").GetComponent<Toggle>().isOn;
                AudioEnabled = GameObject.Find("AudioTrigger").GetComponent<Toggle>().isOn;
            }
        }
    }

    public void SaveOptions()
    {
        // Sets the sound Values
        GameObject.Find("SoundEffectsVolume").GetComponent<Slider>().value = SoundSlider;
        GameObject.Find("MusicVolume").GetComponent<Slider>().value = MusicSlider;
    }

    public void SetOptions()
    {
        // Sets bools to if the player can edit the options
        OptionsSet = true;
        SettingOptions = false;
        InitialSetting = true;
    }

    public void PauseSettingSaves()
    {
        // Sets bools to if the player can edit the options
        SettingOptions = true;
        OptionsSet = false;
        InitialSetting = true;
    }

    public void SaveSoundSlider()
    {
        // Saves the values of thew volume set
        SoundSlider = GameObject.Find("SoundEffectsVolume").GetComponent<Slider>().value;
    }

    public void SaveMusicSlider()
    {
        // Saves the values of thew volume set
        MusicSlider = GameObject.Find("MusicVolume").GetComponent<Slider>().value;
    }

}
