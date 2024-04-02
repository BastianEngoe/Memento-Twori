using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Brightness : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    private Volume volumePostProcess;
    [HideInInspector] public float brightnessValue; //Only for showing in the inspector
    [SerializeField] private GameObject resetButton;
    private float defaultBrightnessSliderValue = 4;
    
    void Start()
    {
        defaultBrightnessSliderValue = 4;
        
        volumePostProcess = Camera.main.GetComponent<Volume>();
        
        if (!brightnessSlider)
        {
            brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
        }

        if (!resetButton)
        {
            resetButton = GameObject.Find("BrightnessResetButton");
        }
        
        if (!PlayerPrefs.HasKey("Brightness"))
        {
            PlayerPrefs.SetFloat("Brightness", 0);
            brightnessValue = 0;
            brightnessSlider.value = brightnessValue + 3;
        }
        else
        {
            brightnessValue = PlayerPrefs.GetFloat("Brightness");
            AdjustBrightness(brightnessValue + 3);
            brightnessSlider.value = brightnessValue + 3;
            resetButton.GetComponent<Button>().interactable = brightnessValue != 3;
        }
        
        if (SceneManager.GetActiveScene().name == "Bootscreen")
        {
            brightnessSlider.interactable = true;
            resetButton.GetComponent<Button>().interactable = true;
        }
    }

    public void AdjustBrightness(float value)
    {
        if (volumePostProcess && volumePostProcess.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.postExposure.value = value - 4;
            brightnessValue = value - 3;
            PlayerPrefs.SetFloat("Brightness", value - 3);
            
            if (value == 3f)
            {
                resetButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                resetButton.GetComponent<Button>().interactable = true;
            }
        }
        
    }
    
    public void ResetBrightness()
    {
        AdjustBrightness(defaultBrightnessSliderValue);
        brightnessSlider.value = defaultBrightnessSliderValue;
    }
}
