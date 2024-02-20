using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighContrast : MonoBehaviour
{
    [SerializeField] private GameObject highContrastToggleButton;
    private CycleSpriteOnClick cycleSpriteOnClick;
    [SerializeField] private GameObject[] objectsToHide, objectsToDarken;
    private Color[] previousColors;
    
    void Start()
    {
        cycleSpriteOnClick = highContrastToggleButton.GetComponent<CycleSpriteOnClick>(); //Using a coroutine because game is paused
        
        if (!highContrastToggleButton)
        {
            highContrastToggleButton = GameObject.Find("HighContrast_Button");
        }
        
        foreach (GameObject obj in objectsToDarken)
        {
            //save the previous color of the object
            previousColors = new Color[objectsToDarken.Length];
            for (int i = 0; i < objectsToDarken.Length; i++)
            {
                previousColors[i] = obj.GetComponent<Image>().color;
            }

        }
        
        if (PlayerPrefs.HasKey("HighContrast"))
        {
            SetHighContrast();
            // Check if the HighContrast value is 0 in PlayerPrefs. If so, change the button sprite to the disabled sprite
            if (PlayerPrefs.GetInt("HighContrast") == 1)
            {
                cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToEnabledSprite());
            }
            else
            {
                cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighContrast", 0);
            cycleSpriteOnClick.StartCoroutine(cycleSpriteOnClick.ChangeToDisabledSprite());
        }
    }

    
    public void ToggleHighContrast()
    {
        // Toggle the value
        PlayerPrefs.SetInt("HighContrast", PlayerPrefs.GetInt("HighContrast") == 0 ? 1 : 0);
        
        SetHighContrast();
    }

    private void SetHighContrast()
    {
        
        //cycle the sprite manually
        //cycleSpriteOnClick.StartCoroutine(PlayerPrefs.GetInt("HighContrast") == 1 ? cycleSpriteOnClick.ChangeToEnabledSprite() : cycleSpriteOnClick.ChangeToEnabledSprite());
        
        
        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(PlayerPrefs.GetInt("HighContrast") == 0);
        }
        
        
        foreach (GameObject obj in objectsToDarken)
        {
            // Get the position of obj in the array
            int position = Array.IndexOf(objectsToDarken, obj);
            
            // Use the position to get the corresponding previous color
            Color previousColor = previousColors[position];
            
            // Change the color of the object based on the HighContrast playerprefs value
            obj.GetComponent<Image>().color = PlayerPrefs.GetInt("HighContrast") == 1 ? new Color(0, 0, 0, 1) : previousColor;
        }
    }
    
}
