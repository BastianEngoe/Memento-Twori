using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class LookSensitivity : MonoBehaviour
{
    [SerializeField] private GameObject lookSensitivitySlider;
    private int lookSensitivity = 5;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        
        if (!lookSensitivitySlider)
        {
            lookSensitivitySlider = GameObject.Find("LookSensitivitySlider");
        }
        
        if (PlayerPrefs.HasKey("LookSensitivity"))
        {
            lookSensitivity = PlayerPrefs.GetInt("LookSensitivity");
            lookSensitivitySlider.GetComponent<UnityEngine.UI.Slider>().value = lookSensitivity;
        }
        else
        {
            lookSensitivity = 5;
            PlayerPrefs.SetInt("LookSensitivity", 5);
            lookSensitivitySlider.GetComponent<UnityEngine.UI.Slider>().value = lookSensitivity;
        }
        SetLookSensitivity(lookSensitivity);
    }

    public void AdjustLookSensitivity(float sliderValue)
    {
        lookSensitivity = (int) sliderValue;
        SetLookSensitivity(lookSensitivity);
    }

    private void SetLookSensitivity(int sensitivity)
    {
        player.GetComponent<FirstPersonController>().RotationSpeed = sensitivity * 0.2f;
        PlayerPrefs.SetInt("LookSensitivity", lookSensitivity);
    }
}
