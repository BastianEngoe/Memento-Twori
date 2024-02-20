using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class BootscreenDialogue : MonoBehaviour
{
    [SerializeField] private AudioClip bulbClip;
    [SerializeField] private TMP_Text dialogue;
    [SerializeField] private BasicDialogueBankScriptableObject dialogueBank;
    private float elapsedTime;
    private int lineIndex, eventIndex, fadeOutCounter;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.unscaledDeltaTime;
            
        if (lineIndex == dialogueBank.bootLines.Count)
        {
            return;
        }
        
        if (dialogueBank.bootLines[lineIndex].duration == 0)
        {
            dialogueBank.bootLines[lineIndex].duration = 3f;
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
        dialogue.text = lineType.dialogue;
        // if (lineType.voiceline)
        // {
        //     GameManager.instance.mascotSpeaker.clip = lineType.voiceline;
        //     GameManager.instance.mascotSpeaker.Play();
        //     lineType.duration = lineType.voiceline.length;
        // }

        if (lineType.triggerEvent)
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
}
