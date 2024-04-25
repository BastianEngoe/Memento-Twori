using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BootscreenDialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogue;
    public BasicDialogueBankScriptableObject dialogueBank;
    [HideInInspector] public int lineIndex;
    [SerializeField] private GameObject brightnessCanvas, screenEffectsCanvas, highContrastCanvas, FOVCanvas;
    private CanvasGroup brightnessCanvasGroup, screenEffectsCanvasGroup, highContrastCanvasGroup, FOVCanvasGroup;
    [SerializeField] private bool skipBootscreen;

    public delegate void LineAdvancedHandler(int lineIndex);
    public event LineAdvancedHandler OnLineHasAdvanced;
    
    
    private void Awake() // Assign the canvas groups to their respective canvas game objects for optimization
    {
        brightnessCanvasGroup = brightnessCanvas.GetComponent<CanvasGroup>();
        screenEffectsCanvasGroup = screenEffectsCanvas.GetComponent<CanvasGroup>();
        highContrastCanvasGroup = highContrastCanvas.GetComponent<CanvasGroup>();
        FOVCanvasGroup = FOVCanvas.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        SetCanvasGroupState(screenEffectsCanvasGroup, false);
        SetCanvasGroupState(brightnessCanvasGroup, false);
        SetCanvasGroupState(highContrastCanvasGroup, false);
        SetCanvasGroupState(FOVCanvasGroup, false);
        if (skipBootscreen)
        {
            dialogue.color = new Color(dialogue.color.r, dialogue.color.g, dialogue.color.b, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void NextLine(BasicDialogueBankScriptableObject.DialogueLine lineType)
    {
        if (skipBootscreen)
        {
            dialogue.color = new Color(dialogue.color.r, dialogue.color.g, dialogue.color.b, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        OnLineHasAdvanced?.Invoke(lineIndex); // Invoke the event to notify subscribers that the dialogue line has advanced. Decouples the dialogue manager from the ClickToContinue script for modularity.
         
        dialogue.text = lineType.dialogue;
        

        if (lineIndex == dialogueBank.bootLines.Count - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (dialogueBank.bootLines[lineIndex].triggersEventWithName != null)
        {
            string eventName = dialogueBank.bootLines[lineIndex].triggersEventWithName;
            eventName = Regex.Replace(eventName, "_", " "); // Replace underscores with spaces
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            eventName = textInfo.ToTitleCase(eventName); // Convert to title case
            eventName = Regex.Replace(eventName, @"\s+", ""); // Remove spaces
            
            Invoke(eventName, 0);
        }
    }
    
    public void ClickToNextLine()
    {
        if (dialogueBank.bootLines[lineIndex].canClickToAdvance) // If the line has autoAdvance set to true, advance to the next line
        {
            lineIndex++;
            NextLine(dialogueBank.bootLines[lineIndex]);
        }
    }

    public void ConfirmButtonToNextLine()
    {
        lineIndex++;
        NextLine(dialogueBank.bootLines[lineIndex]);
    }
    
    public void AdvanceToNextLine()
    {
        lineIndex++;
        NextLine(dialogueBank.bootLines[lineIndex]);
    }
   

    public void ShowBrightnessPanel()
    {
        SetCanvasGroupState(brightnessCanvasGroup, true);
        SetCanvasGroupState(screenEffectsCanvasGroup, false);
        SetCanvasGroupState(highContrastCanvasGroup, false);
        SetCanvasGroupState(FOVCanvasGroup, false);
    }

    public void ShowScreenEffectsPanel()
    {
        SetCanvasGroupState(screenEffectsCanvasGroup, true);
        SetCanvasGroupState(brightnessCanvasGroup, false);
        SetCanvasGroupState(highContrastCanvasGroup, false);
        SetCanvasGroupState(FOVCanvasGroup, false);
    }

    public void ShowHighContrastPanel()
    {
        SetCanvasGroupState(screenEffectsCanvasGroup, false);
        SetCanvasGroupState(brightnessCanvasGroup, false);
        SetCanvasGroupState(highContrastCanvasGroup, true);
        SetCanvasGroupState(FOVCanvasGroup, false);
    }

    public void ShowFOVPanel()
    {
        SetCanvasGroupState(screenEffectsCanvasGroup, false);
        SetCanvasGroupState(brightnessCanvasGroup, false);
        SetCanvasGroupState(highContrastCanvasGroup, false);
        SetCanvasGroupState(FOVCanvasGroup, true);
    }

    private void SetCanvasGroupState(CanvasGroup canvasGroup, bool state)
    {
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
    }
}
