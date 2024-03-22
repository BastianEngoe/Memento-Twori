using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryHole_ConditionSatisfied : MonoBehaviour
{
    public bool batteryNearHole;
    public GameObject battery;
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
