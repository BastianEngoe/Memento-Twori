using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SFXLevel : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Reference to the AudioMixer
    private bool SFXisMuted; // Flag to track mute state of audio mixer groups
    private float volumeBeforeMute;
    [SerializeField] private GameObject SFXSlider;
    [SerializeField] private GameObject SFXToggleButton;
    private CycleSpriteOnClick cycleSpriteOnClick;

    private void Start()
    {

        cycleSpriteOnClick = SFXToggleButton.GetComponent<CycleSpriteOnClick>(); //Using a coroutine because game is paused

        
        if (!SFXSlider)
        {
            SFXSlider = GameObject.Find("SFXSlider");
        }
        if (!SFXToggleButton)
        {
            SFXToggleButton = GameObject.Find("SFX_Button");
        }
        
        if (audioMixer == null)
        {
            Debug.LogWarning("AudioMixer reference is not set in this GameObject " + gameObject.name);
            
            audioMixer = Resources.Load<AudioMixer>("MasterMixer"); //find and assign the audio mixer
            if (audioMixer == null)
            {
                Debug.LogError("Failed to load AudioMixer. Please ensure the AudioMixer is located in the Resources folder and the name is correct.");
            }
        }
        
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            // Get the saved volume from PlayerPrefs and set it to the audio mixer and the slider
            float volume = PlayerPrefs.GetFloat("SFXVolume");
            audioMixer.SetFloat("SFXVolume", volume);
            SFXSlider.GetComponent<Slider>().value = volume;
            
            if (volume <= -39f)
            {
                SFXisMuted = true;
                SFXSlider.GetComponent<Slider>().interactable = false;
                cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
            }
        }
        else
        {
            PlayerPrefs.SetFloat("SFXVolume", 0);
            audioMixer.SetFloat("SFXVolume", 0);
            SFXSlider.GetComponent<Slider>().value = 0;
        }
        
        
    }

    public void ToggleSFX()
    {
        
        
        if (!SFXisMuted)
        {
            audioMixer.GetFloat("SFXVolume", out volumeBeforeMute); // Save the volume before muting, so unmuting goes back to it
        }
        
        SFXisMuted = !SFXisMuted; // Toggle the mute state
        
        SFXSlider.GetComponent<Slider>().interactable = !SFXisMuted;
        
        // Set the volume of the Mixer group based on the mute state
        float volume = SFXisMuted ? -80f : volumeBeforeMute; // Mute volume is typically set to -80dB
        audioMixer.SetFloat("SFXVolume", volume); // Set the volume parameter of the Mixer group
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    
    public void AdjustSFXLevel(float value)
    {
        audioMixer.SetFloat("SFXVolume", value - 30);
        PlayerPrefs.SetFloat("SFXVolume", value - 30);
    }
}
