using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BasicDialogueBankScriptableObject", order = 1)]
public class BasicDialogueBankScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine //doesnt include a conditionMet variable
    {
        public string dialogue;
        public AudioClip voiceline;
    }
    
    [System.Serializable]
    public class DialogueLineDarkness : DialogueLine // Inherit from DialogueLine to add a condition to the dialogue line
    {
        public bool triggerEvent;
        public float duration = 3f;
    }

    [System.Serializable]
    public class DialogueLineBootScreen : DialogueLine // Inherit from DialogueLine to add a condition to the dialogue line
    {
        public string triggersEventWithName;
        public bool canClickToAdvance = true;
    }

    public List<DialogueLineBootScreen> bootLines;
    public List<DialogueLineDarkness> darknessLines;
}
