using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    [SerializeField] private DialogueBankScriptableObject dialogueBank;

    public List<UnityEvent> introEvents;
    public List<UnityEvent> farmEvents;
    public List<UnityEvent> raceEvents;
    public List<UnityEvent> blockEvents;
    public List<UnityEvent> shooterEvents;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerEvent(DialogueBankScriptableObject.DialogueLine eventType, int index)
    {
        //Debug.Log("Received event type " + eventType);

        if (GameManager.instance.curRoom == GameManager.Rooms.INTRO)
        {
            introEvents[index].Invoke();
        }
        
        if (GameManager.instance.curRoom == GameManager.Rooms.FARM)
        {
            farmEvents[index].Invoke();
        }
        
        if (GameManager.instance.curRoom == GameManager.Rooms.RACE)
        {
            raceEvents[index].Invoke();
        }
        
        if (GameManager.instance.curRoom == GameManager.Rooms.BLOCK)
        {
            blockEvents[index].Invoke();
        }
        
        if (GameManager.instance.curRoom == GameManager.Rooms.SHOOTER)
        {
            shooterEvents[index].Invoke();
        }
    }
}