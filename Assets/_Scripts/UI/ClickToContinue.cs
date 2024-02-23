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

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void Start()
    {
        
        canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine(FadeInOut());
        dialogueManager.GetComponent<BootscreenDialogue>().OnLineAdvanced += DialogueLineHasAdvanced; // Subscribe to the event to listen for line advancements
    }

    private IEnumerator FadeInOut()
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
        if (dialogueManager.GetComponent<BootscreenDialogue>().elapsedTime > 1)
        {
            StopAllCoroutines();
            canvasGroup.alpha = 0;
            dialogueManager.GetComponent<BootscreenDialogue>().MeetCurrentCondition();
        }
        
    }
    
    private void DialogueLineHasAdvanced()
    {
        StartCoroutine(FadeInOut());
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

