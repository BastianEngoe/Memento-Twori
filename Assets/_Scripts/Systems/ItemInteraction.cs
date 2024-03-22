using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteraction : MonoBehaviour
{
    public UnityEvent onItemInteraction;
    public GameObject itemPrefab;

    public void compareItem(GameObject itemPrefab)
    {
        //Debug.Log("Held item is: " + itemPrefab.name + " and required item is: " + this.itemPrefab.name);
        if (itemPrefab.name == this.itemPrefab.name)
        {
            onItemInteraction.Invoke();
            Destroy(GetComponent<ItemInteraction>());
        }
    }

    public bool compareItemBool(GameObject itemPrefab)
    {
        if (itemPrefab.name == this.itemPrefab.name)
        {
            return true;
        }

        return false;
    }
}
