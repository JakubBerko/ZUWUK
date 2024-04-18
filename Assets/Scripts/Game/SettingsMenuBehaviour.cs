using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuBehaviour : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] TMP_Dropdown qualityDropdown;


    Resolution[] resolutions;


    private void LoadSettings()
    {
        //if (PlayerPrefs.HasKey("volume")&& PlayerPrefs.HasKey("fullscreen")&& PlayerPrefs.HasKey("graphicsQuality"))
        //{
            //volume
            float volumeIndex = PlayerPrefs.GetFloat("volume");
            volumeSlider.value = volumeIndex;
            SetVolume(volumeIndex);

        //fullscreen
            Screen.fullScreen = false;
            fullscreenToggle.isOn = false;
            //bool fullscreenV = (PlayerPrefs.GetInt("fullscreen") != 0);
            //Debug.Log("naèítám z prefs:" + fullscreenV);
            //fullscreenToggle.isOn = fullscreenV;
            //SetFullscreen(fullscreenV);

            //graphicsQuality
            int qualityIndex = PlayerPrefs.GetInt("graphicsQuality");
            qualityDropdown.value = qualityIndex;
            SetQuality(qualityIndex);

            //resolution

        //}

    }

    private void Start()
    {
        #region Resolutions region
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {            
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                 currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        #endregion

        LoadSettings();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();
    }
    public void SetQuality(int qualityIndex)
    {
        //Debug.Log("Before setting quality level: " + QualitySettings.GetQualityLevel());
        //Debug.Log("Setting quality level to index: " + qualityIndex);
        //QualitySettings.SetQualityLevel(qualityIndex);
        //PlayerPrefs.SetInt("graphicsQuality", QualitySettings.GetQualityLevel());
        //PlayerPrefs.Save();

        //Debug.Log("After setting quality level: " + QualitySettings.GetQualityLevel());
        //Debug.Log("Current anti-aliasing level: " + QualitySettings.antiAliasing);
    }
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("fullscreen", BoolToInt(Screen.fullScreen));
        Debug.Log("Ukládám do prefs:" + BoolToInt(Screen.fullScreen) + "Hodnota isFullscreen je:" + isFullscreen);
        PlayerPrefs.Save();
    }
    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.Save();
    }
    int BoolToInt(bool fullscreen)
    {
        int FCValue;
        if (Screen.fullScreen == true)
        {
            FCValue = 1;
            Debug.Log(FCValue);
        }
        else
        {
            FCValue = 0;
            Debug.Log(FCValue);
        }
        return FCValue;
    }
}

