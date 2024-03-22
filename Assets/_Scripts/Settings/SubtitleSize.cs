using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSize : MonoBehaviour
{
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private GameObject subtitleSizeSlider, subtitlesToggleButton, resetButton;
    private CycleSpriteOnClick cycleSpriteOnClick;
    private bool isFirstCall = true;
    


    private void Start()
    {
        cycleSpriteOnClick = subtitlesToggleButton.GetComponent<CycleSpriteOnClick>(); //Using a coroutine because game is paused
        
        if (!subtitleSizeSlider)
        {
            subtitleSizeSlider = GameObject.Find("SubtitleSlider");
        }
        if (!subtitlesToggleButton)
        {
            subtitlesToggleButton = GameObject.Find("SubtitleSize_Button");
        }
        if (!subtitleText)
        {
            if (GameObject.Find("Dialogue")) //try get the subtitle text from the DialogueManager
            {
                subtitleText = GameObject.Find("Dialogue").GetComponent<TMP_Text>();
            }
            else
            {
                Debug.LogWarning("Subtitle text reference is not set in this GameObject " + gameObject.name);
            }
        }

        if (PlayerPrefs.HasKey("SubtitlesEnabled")) //Checks if playerprefs has SubtitlesEnabled
        {
            if (PlayerPrefs.GetInt("SubtitlesEnabled") == 0) 
            {
                cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
                if (subtitleText) subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
                subtitleSizeSlider.GetComponent<Slider>().interactable = false;
            }
            else
            {
                if (subtitleText) subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 1);
                subtitleSizeSlider.GetComponent<Slider>().interactable = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("SubtitlesEnabled", 1);
        }

        if (PlayerPrefs.HasKey("SubtitleSize"))  //Checks if playerprefs has SubtitleSize
        {
            subtitleSizeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SubtitleSize");
            AdjustSubtitleSize(PlayerPrefs.GetFloat("SubtitleSize"));
            // if (PlayerPrefs.GetFloat("SubtitleSize") == 0)
            // {
            //     cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
            // }
        }
        else
        {
            AdjustSubtitleSize(1);
        }
        
        if (!resetButton)
        {
            resetButton = GameObject.Find("SubtitleSizeResetButton");
        }

    }

    public void AdjustSubtitleSize(float sliderValue) //called from the slider
    {
        if (sliderValue <= 0)
        {
            subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
            cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
            PlayerPrefs.SetInt("SubtitlesEnabled", 0);
            PlayerPrefs.SetFloat("SubtitleSize", 0);
        }
        else
        {
            subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 1);
            cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToEnabledSprite());
            subtitleText.fontSize = 30 + (sliderValue * 6);
            PlayerPrefs.SetInt("SubtitlesEnabled", 1);
            PlayerPrefs.SetFloat("SubtitleSize", sliderValue);
        }
    }

    // private void SetSubtitleSize(float sliderValue) //initially called from Start()
    // {
    //     if (sliderValue <= 0)
    //     {
    //         subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
    //         PlayerPrefs.SetInt("SubtitlesEnabled", 0);
    //         PlayerPrefs.SetFloat("SubtitleSize", 0);
    //     }
    //     else
    //     {
    //         subtitleText.fontSize = 30 + (sliderValue * 6);
    //         PlayerPrefs.SetFloat("SubtitleSize", sliderValue);
    //     }
    //     
    // }

    public void ToggleSubtitlesButton() //called from the button
    {
        PlayerPrefs.SetInt("SubtitlesEnabled", PlayerPrefs.GetInt("SubtitlesEnabled") == 0 ? 1 : 0);
        EnableAndDisableSubtitles();
    }

    private void EnableAndDisableSubtitles() //called from ToggleSubtitles() and at Start()
    {
        if (subtitleSizeSlider.GetComponent<Slider>().value == 0)
        {
            PlayerPrefs.SetInt("SubtitlesEnabled", 1);
            
            AdjustSubtitleSize(1);
            subtitleSizeSlider.GetComponent<Slider>().value = 1;
            
            cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToEnabledSprite());
            
            subtitleSizeSlider.GetComponent<Slider>().interactable = true;
        }
        else
        {
            subtitleSizeSlider.GetComponent<Slider>().interactable = !subtitleSizeSlider.GetComponent<Slider>().interactable;
            subtitleText.color = subtitleText.color.a == 0 ? new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 1) : new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
            if (resetButton)
            {
                resetButton.GetComponent<Button>().interactable = subtitleText.color.a == 1f;
            }
            else
            {
                Debug.LogWarning("Aedan says: Reset button reference is not set in this GameObject " +
                                 gameObject.name);
            }
        }
    }
    
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("SubtitlesEnabled", subtitleSizeSlider.GetComponent<Slider>().interactable ? 1 : 0);
    }

    public void ResetSubtitleSize()
    {
        AdjustSubtitleSize(1);
    }
}
