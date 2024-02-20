using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BasicDialogueBankScriptableObject", order = 1)]
public class BasicDialogueBankScriptableObject : ScriptableObject
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
    
    public List<DialogueLine> bootLines;
    public List<DialogueLine> darknessLines;
}
