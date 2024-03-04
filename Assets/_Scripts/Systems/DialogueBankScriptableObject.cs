using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueBankScriptableObject", order = 1)]
public class DialogueBankScriptableObject : ScriptableObject
{
    public enum conditions
    {
        NONE,
        NODDING,
        EXTERNAL
    }
    
    [System.Serializable]
    public class DialogueLine
    {
        public string dialogue;
        public AudioClip voiceline;
        public float duration = 3f;
        public bool triggerEvent;
        public conditions conditionType;
    }
    
    public List<DialogueLine> introLines;
    public List<DialogueLine> farmLines;
    public List<DialogueLine> raceLines;
    public List<DialogueLine> blockLines;
    public List<DialogueLine> shooterLines;
    public List<DialogueLine> capstoneTutorialLines;
}
