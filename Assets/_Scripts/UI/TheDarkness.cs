using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TheDarkness : MonoBehaviour
{
    [SerializeField] private AudioClip bulbClip;
    [SerializeField] private TMP_Text dialogue;
    [SerializeField] private BasicDialogueBankScriptableObject dialogueBank;
    private float elapsedTime;
    private int lineIndex, eventIndex, fadeOutCounter;

    private void Start()
    {
        dialogue.text = null;
    }


    private void Update()
    {
        if (GetComponent<CanvasGroup>().alpha >= 0.99f)
        {
            elapsedTime += Time.unscaledDeltaTime;
            
            if (lineIndex == dialogueBank.darknessLines.Count)
            {
                return;
            }
        
            if (dialogueBank.darknessLines[lineIndex].duration == 0)
            {
                dialogueBank.darknessLines[lineIndex].duration = 3f;
            }

            if (elapsedTime >= dialogueBank.darknessLines[lineIndex].duration)
            {
                //Disregard conditionMet for this dialogue
                NextLine(dialogueBank.darknessLines[lineIndex]);
                lineIndex++;
                elapsedTime = 0f;
            }
        }
    }
    
    void NextLine(BasicDialogueBankScriptableObject.DialogueLineDarkness lineType)
    {
        dialogue.text = lineType.dialogue;
        
        if (lineType.triggerEvent) // If the line has a trigger event, undarken the screen
        {
            FadeOut();
            lineIndex++;
        }
    }

    public void InvokeTheDarkness()
    {
        StartCoroutine("PlayAudioClip");
    }

    private IEnumerator PlayAudioClip()
    {
        GetComponent<AudioSource>().PlayOneShot(bulbClip);
        GetComponent<Animator>().SetTrigger("Dark");
        yield return new WaitForSecondsRealtime(0.2f);
        GetComponent<AudioSource>().PlayOneShot(bulbClip);
        yield break;
    }

    private void FadeOut()
    {
        fadeOutCounter++;
        // StartCoroutine("FadeOut");
        GetComponent<Animator>().SetTrigger("Light");
        GetComponent<Animator>().ResetTrigger("Dark");
        EventSystem.current.SetSelectedGameObject(null);
        
        if (fadeOutCounter > 2)
        {
            lineIndex = 3;
        }
    }
    
    

    
}
