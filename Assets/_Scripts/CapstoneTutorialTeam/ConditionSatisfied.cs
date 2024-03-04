using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionSatisfied : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name == "roundCube")
            {
                DialogueManager.instance.CheckExternalCondition();
            }
        }

    }
}
