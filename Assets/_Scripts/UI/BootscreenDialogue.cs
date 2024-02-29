using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BootscreenDialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogue;
    public BasicDialogueBankScriptableObject dialogueBank;
    public int lineIndex;
    [SerializeField] private GameObject brightnessCanvas, screenEffectsCanvas;
    public delegate void LineAdvancedHandler();
    public event LineAdvancedHandler OnLineHasAdvanced;
    
    void NextLine(BasicDialogueBankScriptableObject.DialogueLine lineType)
    {
        OnLineHasAdvanced?.Invoke(); // Invoke the event to notify subscribers that the dialogue line has advanced. Decouples the dialogue manager from the ClickToContinue script for modularity.
         
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
    
    public void AdvanceToNextLine()
    {
        lineIndex++;
        NextLine(dialogueBank.bootLines[lineIndex]);
    }
    
    public void ShowBrightnessSlider()
    {
       brightnessCanvas.GetComponent<CanvasGroup>().alpha = 1;
       brightnessCanvas.GetComponent<CanvasGroup>().interactable = true;
       brightnessCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    public void ShowScreenEffectsOption()
    {
        screenEffectsCanvas.GetComponent<CanvasGroup>().alpha = 1;
        screenEffectsCanvas.GetComponent<CanvasGroup>().interactable = true;
        screenEffectsCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
}
