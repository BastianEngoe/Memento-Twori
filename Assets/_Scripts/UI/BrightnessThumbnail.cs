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
    [SerializeField] private int LineIndexInWhichToFadeIn;
    private CanvasGroup canvasGroup;
    
    private void Start()
    {
        if (!dialogueManager) dialogueManager = GameObject.Find("DialogueManager");
        if (dialogueManager) dialogueManager.GetComponent<BootscreenDialogue>().OnLineHasAdvanced += DialogueLineHasHasAdvanced;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void DialogueLineHasHasAdvanced(int lineIndex)
    {
        if (lineIndex == LineIndexInWhichToFadeIn)
        {
            showingBrightnessPanel = true;
            StartCoroutine(UpdateBrightness()); // Start the coroutine to update the brightness
        }
        else
        {
            showingBrightnessPanel = false;
            StopCoroutine(UpdateBrightness());
        }
    }
    
    private IEnumerator UpdateBrightness()
    {
        if (!showingBrightnessPanel)
        {
            yield break;
        }
        while (showingBrightnessPanel)
        {
            if (!PlayerPrefs.HasKey("Brightness")) // If there's no brightness value in PlayerPrefs, set it to 0
            {
                PlayerPrefs.SetFloat("Brightness", 0);
            }

            brightness = PlayerPrefs.GetFloat("Brightness"); // Get the brightness value from PlayerPrefs

            alpha = 0.05f + ((brightness - (-3)) / (3 - (-3))) * (1 - 0.05f); // Calculate the alpha value based on the brightness value

            canvasGroup.alpha = alpha; // Set the alpha value of the image to the calculated alpha value
            Debug.Log("Brightness: " + brightness + " Alpha: " + alpha);

            yield return null; // Wait for the next frame
        }
    }

    // private IEnumerator ShowDelay()
    // {
    //     yield return new WaitForSecondsRealtime(delayBeforeFadeIn);
    //     showingBrightnessPanel = true;
    //     StartCoroutine(ShowBrightnessPanelAfterDelay());
    // }
    //
    // private IEnumerator ShowBrightnessPanelAfterDelay()
    // {
    //     if (!GetComponent<CanvasGroup>()) // If there's no CanvasGroup component, add one
    //     {
    //         gameObject.AddComponent<CanvasGroup>();
    //     }
    //     CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
    //     float elapsedTime = 0f;
    //
    //     StartCoroutine(UpdateBrightness()); // Start the coroutine to update the brightness
    //     
    //     while (elapsedTime < fadeInDuration)
    //     {
    //         canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
    //         elapsedTime += Time.unscaledDeltaTime;
    //         yield return null;
    //     }
    //
    //     canvasGroup.alpha = 1; // Ensure alpha is set to 1 in the end
    // }
    
   
}
