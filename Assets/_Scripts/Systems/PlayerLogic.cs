using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    //Purpose of this script is to change general player mechanics depending on the room state.

    public static PlayerLogic instance;

    [Header("Farming variables")] 
    [SerializeField] private GameObject farmUI;
    public List<Item> inventory;
    [SerializeField] private List<GameObject> inventoryUI;
    [SerializeField] private GameObject inventoryUISelection;
    public int itemIndex;
    [HideInInspector] public Item empty;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (GameManager.instance.curRoom == GameManager.Rooms.FARM)
        {
            farmUI.SetActive(true);
        }
    }

    private void Update()
    {
        switch (GameManager.instance.curRoom)
        {
            case GameManager.Rooms.INTRO: UpdateIntroLogic();
                break;
            
            case GameManager.Rooms.FARM: UpdateFarmLogic();
                break;
            
            case GameManager.Rooms.RACE: UpdateRaceLogic();
                break;
            
            case GameManager.Rooms.BLOCK: UpdateBlockLogic();
                break;
            
            case GameManager.Rooms.SHOOTER: UpdateShooterLogic();
                break;
        }
    }

    private void UpdateIntroLogic()
    {
        
    }

    private void UpdateFarmLogic()
    {
        inventory.Capacity = 6;

        if (Input.mouseScrollDelta.y < -0.5f)
        {
            if (itemIndex == inventory.Count - 1)
            {
                itemIndex = 0;
            }
            else
            {
                itemIndex++;
            }
            
            if (inventory[itemIndex].model != null)
            {
                HeldItem.instance.HoldInventoryItem(inventory[itemIndex].model);
            }
            else
            {
                Destroy(HeldItem.instance.inventoryItem);
                HeldItem.instance.heldItem = null;
            }
        }

        if (Input.mouseScrollDelta.y > 0.5f)
        {
            if (itemIndex == 0)
            {
                itemIndex = inventory.Count - 1;
            }
            else
            {
                itemIndex--;
            }
            
            if (inventory[itemIndex].model != null)
            {
                HeldItem.instance.HoldInventoryItem(inventory[itemIndex].model);
            }
            else
            {
                Destroy(HeldItem.instance.inventoryItem);
                HeldItem.instance.heldItem = null;
            }
        }

        for (int i = 0; i < inventoryUI.Count; i++)
        {
            inventoryUI[i].GetComponent<Image>().sprite = inventory[i].icon;
        }
        
        inventoryUISelection.transform.position = inventoryUI[itemIndex].transform.position;

        if (HeldItem.instance.inventoryItem != null && Input.GetMouseButtonUp(0))
        {
            HeldItem.instance.GetComponent<Animator>().SetTrigger("Use");
            AudioSource hItemSource = HeldItem.instance.GetComponent<AudioSource>();
            hItemSource.clip = inventory[itemIndex].soundEffect;
            hItemSource.Play();
        }
    }

    private void UpdateRaceLogic()
    {
        
    }

    private void UpdateBlockLogic()
    {
        
    }

    private void UpdateShooterLogic()
    {
        
    }
}
