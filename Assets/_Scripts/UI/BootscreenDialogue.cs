using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class BootscreenDialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogue;
    [SerializeField] private BasicDialogueBankScriptableObject dialogueBank;
    public float elapsedTime;
    private int lineIndex, eventIndex, fadeOutCounter;
    public delegate void LineAdvancedHandler();
    public event LineAdvancedHandler OnLineAdvanced;
    private List<bool> initialConditions; // Store the initial conditions of the dialogue lines to reset them when the game is closed or the dialogue is destroyed



    private void Start()
    {
        initialConditions = new List<bool>();
        foreach (var line in dialogueBank.bootLines)
        {
            initialConditions.Add(line.condition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.unscaledDeltaTime;
            
        if (lineIndex == dialogueBank.bootLines.Count)
        {
            return;
        }
        
        if (dialogueBank.bootLines[lineIndex].duration == 0) // Stops the dialogue from progressing too fast if the duration
        {
            dialogueBank.bootLines[lineIndex].duration = 0.2f;
        }

        if (elapsedTime >= dialogueBank.bootLines[lineIndex].duration)
        {
            if (!dialogueBank.bootLines[lineIndex].condition)
            {
                return;
            }
            NextLine(dialogueBank.bootLines[lineIndex]);
            lineIndex++;
            elapsedTime = 0f;
        }
    }
    
    
    void NextLine(BasicDialogueBankScriptableObject.DialogueLine lineType)
    {
        OnLineAdvanced?.Invoke(); // Invoke the event to notify subscribers that the dialogue line has advanced. Decouples the dialogue manager from the ClickToContinue script for modularity.
         
        dialogue.text = lineType.dialogue;
        // if (lineType.voiceline)
        // {
        //     GameManager.instance.mascotSpeaker.clip = lineType.voiceline;
        //     GameManager.instance.mascotSpeaker.Play();
        //     lineType.duration = lineType.voiceline.length;
        // }

        if (lineType.triggerEvent && lineIndex > 5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // if (!lineType.condition)
        // {
        //     InvokeRepeating("CheckForCondition", 0f, 0.25f);
        // }
        
        
        if (lineIndex > 2) // Check if line index is greater than two
        {
            
            Cursor.lockState = CursorLockMode.None; // Change cursor lock mode to unlocked
        }
    }
    
    public void MeetCurrentCondition()
    {
        if (!dialogueBank.bootLines[lineIndex].condition)
        {
            dialogueBank.bootLines[lineIndex].condition = true;
        }
    }

    public void OnApplicationQuit()
    {
        ResetConditions();
    }

    private void OnDestroy()
    {
        ResetConditions();
    }

    private void ResetConditions()
    {
        // Reset the conditions to their initial state
        for (int i = 0; i < dialogueBank.bootLines.Count; i++)
        {
            dialogueBank.bootLines[i].condition = initialConditions[i];
        }
    }
}
