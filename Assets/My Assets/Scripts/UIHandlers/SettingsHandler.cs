using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle musicToggle;
    private GameObject musicBox;

    private void Awake()
    {
        musicToggle.isOn = Settings.musicIsOn;
        volumeSlider.value = Settings.musicVolume;
        musicBox = GameObject.Find("MusicBox");

        volumeSlider.onValueChanged.AddListener(ValueChangeCheck);
        musicToggle.onValueChanged.AddListener(ValueChangeCheck);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ValueChangeCheck(float param)
    {
        Settings.musicVolume = volumeSlider.value;
        if (musicBox != null)
        {
            musicBox.GetComponent<AudioSource>().volume = Settings.musicVolume;
        }
    }

    public void ValueChangeCheck(bool param)
    {
        Settings.musicIsOn = param;
        if (musicBox != null)
        {
            musicBox.GetComponent<AudioSource>().mute = !Settings.musicIsOn;
        }
    }
}
