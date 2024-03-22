using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BootscreenConfirmButton : MonoBehaviour
{
    [SerializeField] private BootscreenDialogue dialogueManager;
    private Vector2 originalScale;
    private RectTransform rectTransform;
    
    private void Start()
    {
        if (!dialogueManager)
        {
            dialogueManager = GameObject.Find("DialogueManager").GetComponent<BootscreenDialogue>();
        }
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }
    
    public void ConfirmButtonPress()
    {
        dialogueManager.ConfirmButtonToNextLine();
    }

    public void EnlargeScale(float scale = 1.1f)
    {
        rectTransform.localScale = new Vector2(scale, scale);
    }

    public void ScaleBackToOriginal()
    {
        rectTransform.localScale = originalScale;
    }
    
    
    // For controller support
    //
    // public void OnSelect(BaseEventData eventData)
    // {
    //     // This code will be executed when the button is selected
    //     Debug.Log("Button selected");
    // }
    
}
