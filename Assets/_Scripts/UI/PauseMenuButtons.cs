using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenuButtons : MonoBehaviour
{
    private float pauseSymbolTimeElapsed, buttonScaleTimeElapsed;
    private float pauseSymbolLerpDuration = 0.7f; //Change this to change the time it takes to flash the pause icon
    private Color pauseSymbolColor1, pauseSymbolColor2;
    private bool hoveringOverButton;
    private Vector2 scale1, scale2;
    private int timesLerpedButtonScale;
    private GameObject settingsPanel;
    private Volume _volume; // URP Volume

    #region ToggleSFX variables
    [HideInInspector] public AudioMixer audioMixer; // Reference to the AudioMixer
    private bool SFXisMuted, MusicisMuted; // Flag to track mute state of audio mixer groups
    private float savedSFXVolume, savedMusicVolume;
    #endregion

    
    
    void Start()
    {
        pauseSymbolColor1 = new Color(1, 1, 1, 0);
        pauseSymbolColor2 = new Color(1, 1, 1, 1);
        scale1 = new Vector2(1f,1f);
        scale2 = new Vector2(1.05f,1.05f);
        
        
        if (name.Contains("Music") | name.Contains("SFX")) // Ensure that the audioMixer reference is set
        {
            if (audioMixer == null)
            {
                Debug.LogError("AudioMixer reference is not set in this GameObject " + gameObject.name);
            }
        }
        
        ResizeButtonSize();
    }

    
    private void Update()
    {
        if (name == "Paused_Icon" && Time.timeScale == 0)
        {
            PauseSymbolLerp();
        }

        if (hoveringOverButton)
        {
            HoverButtonLerp();
        }
    }
    
    
    private void PauseSymbolLerp()
    {
        if (pauseSymbolTimeElapsed < pauseSymbolLerpDuration)
        {
            GetComponent<Image>().color = Color.Lerp(pauseSymbolColor1, pauseSymbolColor2, pauseSymbolTimeElapsed / pauseSymbolLerpDuration);
            //GetComponent<Image>().color = Color.Lerp(pauseSymbolColor1, pauseSymbolColor2, pauseSymbolTimeElapsed / pauseSymbolLerpDuration);
            pauseSymbolTimeElapsed += Time.unscaledDeltaTime;
        }
        else 
        {
            GetComponent<Image>().color = pauseSymbolColor2;
            //GetComponent<Image>().color = pauseSymbolColor2;
            (pauseSymbolColor1, pauseSymbolColor2) = (pauseSymbolColor2, pauseSymbolColor1);

            pauseSymbolTimeElapsed = 0;
        }
    }


    public void QuitGame() // make this funny, move the quit button after the "Are you sure?"
    {
        Application.Quit();
    }

    public void OpenSettingsMenu()
    {
        if (!settingsPanel)
        {
            settingsPanel = GameObject.Find("SettingsMenu");
        }

        settingsPanel.GetComponent<Animator>().SetInteger("Direction", 0);
        
        settingsPanel.GetComponent<Animator>().SetTrigger("OpenSettings");

        Cursor.visible = true;
    }

    public void CloseSettingsMenu()
    {
        if (!settingsPanel)
        {
            settingsPanel = GameObject.Find("SettingsMenu");
        }
        settingsPanel.GetComponent<Animator>().SetInteger("Direction",Random.Range(1,5));
        settingsPanel.GetComponent<Animator>().ResetTrigger("OpenSettings");
        
        ResizeButtonSize();
        
    }

    public void HoverButtonEnter()
    {
        hoveringOverButton = true;
        timesLerpedButtonScale = 0;

    }

    public void HoverButtonExit()
    {
        hoveringOverButton = false;
        GetComponent<RectTransform>().localScale = scale1;
        buttonScaleTimeElapsed = 0;
    }


    private void HoverButtonLerp()
    {
        if (buttonScaleTimeElapsed < (pauseSymbolLerpDuration / 5f)) //This lerp takes 1/3 of the time to bounce compared to the icon
        {
            GetComponent<RectTransform>().localScale = Vector2.Lerp(scale1, scale2, buttonScaleTimeElapsed / (pauseSymbolLerpDuration / 5f));
            buttonScaleTimeElapsed += Time.unscaledDeltaTime;
        }
        else 
        {
            GetComponent<RectTransform>().localScale = scale2;

            (scale1, scale2) = (scale2, scale1);

            buttonScaleTimeElapsed = 0;
            
            timesLerpedButtonScale++;

            if (timesLerpedButtonScale == 2)
            {
                hoveringOverButton = false;
            }
        }
    }


    public void ToggleSFX() // Method to toggle the mute state of the Mixer group
    {
        
        if (!SFXisMuted)
        {
            audioMixer.GetFloat("SFXVolume", out savedSFXVolume); // Save the volume before muting, so unmuting goes back to it
        }
        
        SFXisMuted = !SFXisMuted; // Toggle the mute state
        
        // Set the volume of the Mixer group based on the mute state
        float volume = SFXisMuted ? -80f : savedSFXVolume; // Mute volume is typically set to -80dB
        audioMixer.SetFloat("SFXVolume", volume); // Set the volume parameter of the Mixer group
    }

    private void ResizeButtonSize()
    {
        if (!PlayerPrefs.HasKey("SubtitleSize"))
        {
           
        }
        
    }
    
}
