using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    [SerializeField] private GameObject hoePrefab, seedPrefab, wateringCanPrefab;
    private Animator anim;
    private ItemInteraction itemInteraction;
    public enum plantStates
    {
        NONE,
        HOED,
        SEEDED,
        WATERED
    }

    public plantStates curPlantState;

    public void UpdatePlant()
    {
        Debug.Log("updating plant");
        anim = GetComponentInParent<Animator>();
        itemInteraction = GetComponent<ItemInteraction>();

        switch (curPlantState)
        {
            case plantStates.NONE:
                if(itemInteraction.compareItemBool(hoePrefab))
                anim.SetTrigger("Step1");
                curPlantState = plantStates.HOED;
                break;
            
            case plantStates.HOED:
                if(itemInteraction.compareItemBool(seedPrefab))
                curPlantState = plantStates.SEEDED;
                break;
            
            case plantStates.SEEDED:
                if(itemInteraction.compareItemBool(wateringCanPrefab))
                anim.SetTrigger("Step2");
                curPlantState = plantStates.WATERED;
                break;
            
            case plantStates.WATERED:
                break;
        }
    }
}
