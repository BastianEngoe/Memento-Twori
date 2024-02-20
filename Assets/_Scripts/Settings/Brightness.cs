using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Brightness : MonoBehaviour
{
    [SerializeField] private Slider brightnessSlider;
    private Volume volumePostProcess;
    [HideInInspector] public float brightnessValue; //Only for showing in the inspector
    [SerializeField] private GameObject resetButton;
    
    void Start()
    {
        volumePostProcess = GameObject.Find("MainCamera").GetComponent<Volume>();
        
        if (!brightnessSlider)
        {
            brightnessSlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
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
            resetButton.GetComponent<Button>().interactable = brightnessValue != 0;
        }
    }

    public void AdjustBrightness(float value)
    {
        if (volumePostProcess && volumePostProcess.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.postExposure.value = value - 3;
            brightnessValue = value - 3;
            PlayerPrefs.SetFloat("Brightness", value - 3);
            
            if (value != 3f)
            {
                resetButton.GetComponent<Button>().interactable = true;
            }
        }
        
    }
    
    public void ResetBrightness()
    {
        brightnessValue = 0;
        brightnessSlider.value = 3;
        PlayerPrefs.SetFloat("Brightness", 0);
        AdjustBrightness(0);
        resetButton.GetComponent<Button>().interactable = false;
    }
}
