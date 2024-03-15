using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootscreenConfirmButton : MonoBehaviour
{
    [SerializeField] private BootscreenDialogue dialogueManager;
    
    private void Start()
    {
        if (!dialogueManager)
        {
            dialogueManager = GameObject.Find("DialogueManager").GetComponent<BootscreenDialogue>();
        }
    }
    
    public void ConfirmButtonPress()
    {
        dialogueManager.ClickToNextLine();
    }
    
    
}
