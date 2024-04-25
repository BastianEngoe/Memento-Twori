using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class ClickToContinue : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private GameObject dialogueManager;
    private PlayerInputActions playerControls;
    private InputAction advanceDialogue;
    private bool canAdvance;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void Start()
    {
        
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeInAndOut());
        dialogueManager.GetComponent<BootscreenDialogue>().OnLineHasAdvanced += DialogueLineHasHasAdvanced; // Subscribe to the event to listen for line advancements
    }

    private IEnumerator FadeInAndOut()
    {
        yield return new WaitForSecondsRealtime(2);
        
        while (true)
        {
            canAdvance = true; // Allow the player to advance the dialogue
            
            
            for (float t = 0; t <= 1; t += 2 * Time.unscaledDeltaTime) // Fade in
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, t);
                yield return null;
            }
            
            yield return new WaitForSecondsRealtime(0.25f);
            
            
            for (float t = 0; t <= 1; t += 2 * Time.unscaledDeltaTime) // Fade out
            {
                canvasGroup.alpha = Mathf.Lerp(1, 0, t);
                yield return null;
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
    
    public void ClickToSkip(InputAction.CallbackContext context)
    {
        if (canAdvance)
        {
            StopAllCoroutines();
            canvasGroup.alpha = 0;
            dialogueManager.GetComponent<BootscreenDialogue>().ClickToNextLine();
            canAdvance = false;
        }
        
    }
    
    private void DialogueLineHasHasAdvanced(int lineIndex)
    {
        if (dialogueManager.GetComponent<BootscreenDialogue>().dialogueBank.bootLines[dialogueManager.GetComponent<BootscreenDialogue>().lineIndex].canClickToAdvance)
        {
            StartCoroutine(FadeInAndOut()); // If the line has autoAdvance set to true, fade in and out
        }
    }
    
    private void OnEnable()
    {
        advanceDialogue = playerControls.Misc.AdvanceDialogue;
        advanceDialogue.Enable();
        advanceDialogue.performed += ClickToSkip;
    }
    
    private void OnDisable()
    {
        advanceDialogue.Disable();
    }
}

