using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootscreenPhaseFadeIn : MonoBehaviour
{
    [SerializeField] private GameObject dialogueManager;
    [SerializeField] private int lineIndexAtWhichToFadeIn;
    [SerializeField] private float delayBeforeFadeIn = 2f;
    [SerializeField] private float fadeInDuration = 1f;
    private CanvasGroup canvasGroup;
    
    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        
        if (!canvasGroup)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        
        canvasGroup.alpha = 0; // Ensure alpha is set to 0 at the start
        
        if (!dialogueManager) dialogueManager = GameObject.Find("DialogueManager");
        if (dialogueManager) dialogueManager.GetComponent<BootscreenDialogue>().OnLineHasAdvanced += DialogueLineHasHasAdvanced;
        
    }
    
    private void DialogueLineHasHasAdvanced(int lineIndex)
    {
        if (lineIndex != lineIndexAtWhichToFadeIn) return; // If the line index isn't the one we want to fade in on, don't do anything
        StartCoroutine(ShowBrightnessPanelAfterDelay());
    }
    
    
    private IEnumerator ShowBrightnessPanelAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayBeforeFadeIn);

        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1; // Ensure alpha is set to 1 at the end
    }
}
