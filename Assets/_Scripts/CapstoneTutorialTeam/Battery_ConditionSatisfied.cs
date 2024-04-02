using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery_ConditionSatisfied : MonoBehaviour
{
    public bool canPickUp, hasPickedUp, canPutDown, hasPutDown;
    public DialogueManager dialogueManager;

    private void Start()
    {
        if (dialogueManager == null)
        {
            dialogueManager = DialogueManager.instance;
        }
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.E) && canPickUp)
        {
            if (dialogueManager.lineIndex == 16)
            {
                Debug.Log("External condition should be checked");
                DialogueManager.instance.CheckExternalCondition();
            }
            hasPickedUp = true;
            canPickUp = false;
            canPutDown = true;
        }
        if (dialogueManager.lineIndex == 17)
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                DialogueManager.instance.CheckExternalCondition();
                hasPutDown = true;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("PickUp"))
            {
                canPickUp = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("PickUp"))
            {
                canPickUp = false;
            }
        }
    }
}
