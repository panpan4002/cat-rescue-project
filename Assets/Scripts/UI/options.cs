using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class options : MonoBehaviour
{
    public GameObject optionsPanel;
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private float volume;

    [Header("Old Config")]
    private int qualityO;
    private float volumeO;
    private bool fullScreenO;
    private int resolutionO;

    void Awake()
    {
        //audioMixer.GetFloat("masterVolume", out volume);
        //setVolume(volume);
    }
    void Start()

    {

        //volumeO = volume;
        fullScreenO = Screen.fullScreen;
        qualityO = QualitySettings.GetQualityLevel();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resolutionsList = new List<string>();

        int currentResolution = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = resolutions[i].width + "x" + resolutions[i].height;
            resolutionsList.Add(resolutionOption);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
                resolutionO = currentResolution;
            }
        }

        resolutionDropdown.AddOptions(resolutionsList);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    void Update()
    {
        
    }

    public void optionsScreen(bool active)
    {
        optionsPanel.SetActive(active);
        //if(active) fullScreenToggle.colors.normalColor = 
    }

    public void optionsBack()
    {
        //audioMixer.SetFloat("masterVolume", MathF.Log10(volumeO) * 20);
        resolutionDropdown.value = resolutionO;
        Screen.fullScreen = fullScreenO;
        QualitySettings.SetQualityLevel(qualityO);
        optionsPanel.SetActive(false);
    }

    public void setResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", MathF.Log10(volume) * 20);
    }

    public void setQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);   
    }

    public void setFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        Debug.Log(fullScreen);
    }
}
