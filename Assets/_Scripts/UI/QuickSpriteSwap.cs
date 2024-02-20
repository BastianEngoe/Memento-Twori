using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSpriteSwap : MonoBehaviour
{

    public Sprite[] sprites;
    public float timeBetweenSwaps;
    private int currentIndex = 0; // Index of the current sprite being displayed
    private float timer = 0f; // Timer to keep track of time elapsed

    
    void Start()
    {
        // Check if there are sprites assigned
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites assigned to QuickSpriteSwap script on GameObject " + gameObject.name);
            enabled = false; // Disable the script
            return;
        }

        // Set the initial sprite
        GetComponent<UnityEngine.UI.Image>().sprite = sprites[currentIndex];
    }

    
    void Update()
    {
        if (PlayerPrefs.GetInt("ScreenEffects") == 0) return; // If screen effects are disabled, stop the script
            
        // Increment the timer
        timer += Time.unscaledDeltaTime;

        // Check if it's time to swap sprites
        if (timer >= timeBetweenSwaps)
        {
            // Reset the timer
            timer = 0f;

            // Increment the index
            currentIndex++;

            // Check if we reached the end of the array
            if (currentIndex >= sprites.Length)
            {
                currentIndex = 0; // Reset to the first sprite
            }

            // Swap the sprite
            GetComponent<UnityEngine.UI.Image>().sprite = sprites[currentIndex];
        }
    }
}

