using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_ConditionSatisfied : MonoBehaviour
{
    public bool hasTouchedCube;
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
}
