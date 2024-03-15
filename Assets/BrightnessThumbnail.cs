using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class BrightnessThumbnail : MonoBehaviour
{
    private float brightness;
    private float alpha;
    [SerializeField] private GameObject dialogueManager;
    private bool showingBrightnessPanel;
    
    private void Start()
    {
        if (!dialogueManager) dialogueManager = GameObject.Find("DialogueManager");
        if (dialogueManager) dialogueManager.GetComponent<BootscreenDialogue>().OnLineHasAdvanced += DialogueLineHasHasAdvanced;
        
    }

    private void DialogueLineHasHasAdvanced(int lineIndex)
    {
        if (lineIndex == 1)
        {
            StartCoroutine(ShowBrightnessPanelAfterDelay());
        }else
        {
            showingBrightnessPanel = false;
        }
    }

    private IEnumerator ShowBrightnessPanelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(2);
        showingBrightnessPanel = true;

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / duration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1; // Ensure alpha is set to 1 at the end
    }
    
    private void Update()
    {
        if (!showingBrightnessPanel) return; // If the brightness panel isn't showing, don't do anything
        
        if (!PlayerPrefs.HasKey("Brightness")) // If there's no brightness value in PlayerPrefs, don't do anything
        {
            Debug.Log("No Brightness value in PlayerPrefs.");
            return;
        }
        
        brightness = PlayerPrefs.GetFloat("Brightness"); // Get the brightness value from PlayerPrefs
        
        alpha = 0.05f + ((brightness - (-3)) / (3 - (-3))) * (1 - 0.05f); // Calculate the alpha value based on the brightness value

        GetComponent<Image>().color = new Color(1, 1, 1, alpha); // Set the alpha value of the image to the calculated alpha value
    }
    
}
