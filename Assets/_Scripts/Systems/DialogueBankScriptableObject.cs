using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueBankScriptableObject", order = 1)]
public class DialogueBankScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class DialogueLine
    {
        public string dialogue;
        public AudioClip voiceline;
        public float duration = 3f;
        public bool triggerEvent;
        public bool condition = true;
    }
    
    public List<DialogueLine> introLines;
    public List<DialogueLine> farmLines;
    public List<DialogueLine> raceLines;
    public List<DialogueLine> blockLines;
    public List<DialogueLine> shooterLines;
}
