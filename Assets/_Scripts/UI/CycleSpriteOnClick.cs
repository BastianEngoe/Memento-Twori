using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CycleSpriteOnClick : MonoBehaviour
{
    private Button button; // Reference to the Button component
    public Sprite[] highlightedSprite, pressedSprite; // Array of sprites to cycle through
     public int currentIndex; // Index of the current sprite being displayed

    
    void Start()
    {
        button = GetComponent<Button>();
        
        // Ensure that the button reference is set
        if (button == null)
        {
            Debug.LogError("Button reference is not set in CycleButtonSprites script on GameObject " + gameObject.name);
            enabled = false; // Disable the script
        }
    }

    // Method to cycle to the next sprite in the array
    public void CycleToNextSprite()
    {
        // Increment the index
        currentIndex++;

        // Check if we reached the end of the array
        if (currentIndex >= highlightedSprite.Length)
        {
            currentIndex = 0; // Reset to the first sprite
        }
        
        // Display the sprite at the current index for each state of the button
        GetComponent<Image>().sprite = pressedSprite[currentIndex];
        
        SpriteState spriteState = new SpriteState();
        spriteState.highlightedSprite = highlightedSprite[currentIndex];
        spriteState.pressedSprite = pressedSprite[currentIndex];
        button.spriteState = spriteState;
        
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public IEnumerator ChangeToEnabledSprite()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        currentIndex = 1;
        CycleToNextSprite();
    }
    
    public IEnumerator ChangeToDisabledSprite()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        currentIndex = 0;
        CycleToNextSprite();
    }
}
