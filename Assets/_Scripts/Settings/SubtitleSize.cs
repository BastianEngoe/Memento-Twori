using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSize : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text subtitleText;
    [SerializeField] private GameObject subtitleSizeSlider, subtitlesToggleButton;
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

        if (PlayerPrefs.HasKey("SubtitlesEnabled"))
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

        if (PlayerPrefs.HasKey("SubtitleSize"))
        {
            subtitleSizeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("SubtitleSize");
            SetSubtitleSize(PlayerPrefs.GetFloat("SubtitleSize"));
            if (PlayerPrefs.GetFloat("SubtitleSize") == 0)
            {
                cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
            }
        }
        else
        {
            PlayerPrefs.SetFloat("SubtitleSize", 1);
            subtitleSizeSlider.GetComponent<Slider>().value = 1;
            subtitleSizeSlider.GetComponent<Slider>().interactable = true;
            subtitleText.fontSize = 36;
        }

    }

    public void AdjustSubtitleSize(float sliderValue) //called from the slider
    {
        if (isFirstCall)
        {
            Debug.Log("Slider just decided to adjust on its own.");
            isFirstCall = false;
            return;
        }
        
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

    private void SetSubtitleSize(float sliderValue) //initially called from Start()
    {
        if (sliderValue <= 0)
        {
            subtitleText.color = new Color(subtitleText.color.r, subtitleText.color.g, subtitleText.color.b, 0);
            PlayerPrefs.SetInt("SubtitlesEnabled", 0);
            PlayerPrefs.SetFloat("SubtitleSize", 0);
        }
        else
        {
            subtitleText.fontSize = 30 + (sliderValue * 6);
            PlayerPrefs.SetFloat("SubtitleSize", sliderValue);
        }
        
    }

    public void ToggleSubtitles() //called from the button
    {
        PlayerPrefs.SetInt("SubtitlesEnabled", PlayerPrefs.GetInt("SubtitlesEnabled") == 0 ? 1 : 0);
        SetSubtitles();
    }

    private void SetSubtitles()
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
        }
    }
    
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("SubtitlesEnabled", subtitleSizeSlider.GetComponent<Slider>().interactable ? 1 : 0);
    }
}
