using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionSatisfied : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Object noticed player");
            if (this.gameObject.name == "roundCube")
            {
                DialogueManager.instance.CheckExternalCondition();
                Debug.Log("The cube noticed the player");
            }

            if (this.gameObject.CompareTag("PickUp"))
            {
                if (Input.GetKeyUp(KeyCode.E)) 
                {
                    DialogueManager.instance.CheckExternalCondition();
                }
                if (Input.GetKeyUp(KeyCode.Q))
                {
                    DialogueManager.instance.CheckExternalCondition();
                }
            }
        }

    }
}
