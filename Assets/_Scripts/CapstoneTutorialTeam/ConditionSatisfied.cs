using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionSatisfied : MonoBehaviour
{
    public bool hasTouchedCube, canPickUp, hasPickedUp, canPutDown, hasPutDown, batteryNearHole;
    public GameObject currentHeldItem, battery;
    public Animator batteryHoleAnimator, doorAnimator;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && canPickUp)
        {
            Debug.Log("External condition should be checked");
            DialogueManager.instance.CheckExternalCondition();
            hasPickedUp = true;
            canPickUp = false;
            canPutDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Q) && hasPutDown == false && canPutDown)
        {
            DialogueManager.instance.CheckExternalCondition();
            hasPutDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Q) && batteryNearHole)
        {
            Debug.Log("Should play animation");
            if (batteryHoleAnimator != null)
            {
                Debug.Log("Is playing animation");
                batteryHoleAnimator.Play("Insertbattery");
                Destroy(battery);
                doorAnimator.Play("DoorOpen");
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Object noticed player");
            if (this.gameObject.name == "roundCube" && hasTouchedCube == false)
            {
                hasTouchedCube = true;
                DialogueManager.instance.CheckExternalCondition();
                Debug.Log("The cube noticed the player");
            }
        }

    }

    public void DoExternalCondition()
    {
        DialogueManager.instance.CheckExternalCondition();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.CompareTag("PickUp"))
            {
                canPickUp = true;
            }
        } 
        if (this.gameObject.name == "BatteryHole")
        {
            Debug.Log("Hole knows its a battery hole");
            currentHeldItem = GameObject.Find("HeldItem").GetComponent<HeldItem>().heldItem;
            batteryNearHole = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.gameObject.name == "BatteryHole")
        {
            batteryNearHole = false;
        }
    }
}
