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
            // Fade in
            for (float t = 0; t <= 1; t += 2 * Time.unscaledDeltaTime)
            {
                canvasGroup.alpha = Mathf.Lerp(0, 1, t);
                yield return null;
            }

            yield return new WaitForSecondsRealtime(0.25f);
            canAdvance = true;

            // Fade out
            for (float t = 0; t <= 1; t += 2 * Time.unscaledDeltaTime)
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
    
    private void DialogueLineHasHasAdvanced()
    {
        if (dialogueManager.GetComponent<BootscreenDialogue>().dialogueBank.bootLines[dialogueManager.GetComponent<BootscreenDialogue>().lineIndex].canClickToAdvance)
        {
            StartCoroutine(FadeInAndOut());
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

