using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionSatisfied : MonoBehaviour
{
    public bool canPickUp, hasPickedUp, canPutDown, hasPutDown, batteryNearHole;
    public GameObject currentHeldItem, battery;
    public Animator batteryHoleAnimator, doorAnimator;
    private void Update()
    {
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
   

    private void OnTriggerStay(Collider other)
    {
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
