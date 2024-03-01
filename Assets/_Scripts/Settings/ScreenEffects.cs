using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Cinemachine;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenEffects : MonoBehaviour
{
    [HideInInspector] public bool screenEffectsEnabled; // Variable to store the ScreenEffects value
    [SerializeField] private GameObject screenFXButton; // Reference to the ScreenFX button
    private Volume postProcessVolume;
    private Camera mainCam;
    private UniversalAdditionalCameraData UAC;
    private VolumeProfile postProcessVolumeProfile;
    private CycleSpriteOnClick cycleSpriteOnClick;
    private GameObject playerFollowCamera;
    private float previousNoiseAmplitude;
    [SerializeField] private GameObject pausedIcon;
    


    private void Start()
    {
        cycleSpriteOnClick = screenFXButton.GetComponent<CycleSpriteOnClick>(); //Using a coroutine because game is paused
        
        mainCam = Camera.main;
        if(SceneManager.GetActiveScene().buildIndex == 0) UAC = mainCam.GetComponent<UniversalAdditionalCameraData>();
        if(SceneManager.GetActiveScene().buildIndex == 0) postProcessVolume = mainCam.GetComponent<Volume>();
        if(SceneManager.GetActiveScene().buildIndex == 0) postProcessVolumeProfile = postProcessVolume.profile;
        
        if (!screenFXButton)
        {
            screenFXButton = GameObject.Find("ScreenFX_Button");
            if (!screenFXButton)
            {
                Debug.LogWarning("ScreenFX_Button reference is not set in this GameObject " + gameObject.name);
            }

        }

        if (!pausedIcon)
        {
            pausedIcon = GameObject.Find("Paused_Icon");
            
            if (!pausedIcon)
            {
                
            }
        }
        
        if (!PlayerPrefs.HasKey("ScreenEffects"))
        {
            PlayerPrefs.SetInt("ScreenEffects", 1);
        }
        
        if (playerFollowCamera == null)
        {
            playerFollowCamera = GameObject.Find("PlayerFollowCamera");
        }
        
        screenEffectsEnabled = PlayerPrefs.GetInt("ScreenEffects") == 1;
        
        
        SetScreenFX();
        
        if (!screenEffectsEnabled)
        {
            cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
        }        
    }
    
    public void ToggleScreenFX()
    {
        // Toggle the value
        screenEffectsEnabled = !screenEffectsEnabled;
        
        // Save the value
        SetScreenFX();
    }

    private void SetScreenFX() // Method to save the ScreenEffects value to PlayerPrefs
    {
        PlayerPrefs.SetInt("ScreenEffects", screenEffectsEnabled ? 1 : 0);

        if (postProcessVolume.profile.TryGet(out ScreenSpaceLensFlare screenSpaceLensFlare))
        {
            screenSpaceLensFlare.intensity.value = screenEffectsEnabled ? 0.3f : 0;
        }
            
        if (postProcessVolume.profile.TryGet(out Bloom bloom))
        {
            bloom.intensity.value = screenEffectsEnabled ? 0.5f : 0;
        }
            
        if (postProcessVolume.profile.TryGet(out MotionBlur motionBlur))
        {
            motionBlur.intensity.value = screenEffectsEnabled ? 0.3f : 0;
        }

        if (playerFollowCamera == null)
        {
            playerFollowCamera = GameObject.Find("PlayerFollowCamera");
        }
        
        if (playerFollowCamera != null)
        {
            playerFollowCamera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = screenEffectsEnabled ? 0.5f : 0;
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                Debug.LogError("PlayerFollowCamera GameObject is not found or not active.");
            }
        }
        
        if (pausedIcon) pausedIcon.GetComponent<PauseMenuButtons>().enabled = screenEffectsEnabled;
        if (pausedIcon) pausedIcon.GetComponent<Image>().color = screenEffectsEnabled ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
    }
    
}
