using System.Collections;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// TO (potentially) DO:
///
/// Cry about it.
/// </summary>

public class HeldItem : MonoBehaviour
{
    public static HeldItem instance;
    
    [SerializeField] private bool holdingItem = false;
    public GameObject heldItem;
    public List<GameObject> potentialPickups;
    public bool canPickup = true;

    [SerializeField] private ItemDatabaseScriptableObject items;

    [SerializeField] private Material outlineShader;
    [SerializeField] private Material[] matArray;
    [SerializeField] private List<Material> matArray2;

    public GameObject inventoryItem;

    private void Awake()
    {
        instance = this;
    }

    public void PickupItem(GameObject item)
    {
        if (GameManager.instance.curRoom == GameManager.Rooms.INTRO)
        {
            if(!canPickup)  return;
        
            //If already holding an item, drop it first.
            if (heldItem)
            {
                DropItem();
            }
        
            //Disable movement while picking up.
            GameManager.instance.ToggleMovement(false);
        
            //Assign found item as currently held item.
            heldItem = item;

            if(heldItem.TryGetComponent(out MeshRenderer mRend))
            {
                matArray2 = matArray.ToList();
            
                matArray2.Remove(matArray2[1]);

                matArray = matArray2.ToArray();

                mRend.materials = matArray;
            }

            //Move the item in place, re-enable movement and set items transform parent.
            heldItem.transform.DOMove(transform.position, 0.75f).OnComplete
                ((() => GameManager.instance.ToggleMovement(true)));
            heldItem.transform.DORotate(transform.rotation.eulerAngles, 0.5f);
            heldItem.transform.parent = transform;
        
            //If item has a Collider and/or Rigidbody, disable them while held.
            if(heldItem.TryGetComponent(out Rigidbody itemRB))
            {
                itemRB.useGravity = false;
                itemRB.velocity = Vector3.zero;
            }

            if (heldItem.TryGetComponent(out Collider itemCollider))
            {
                itemCollider.enabled = false;
            }

            //Now holding item, yay!
            holdingItem = true;

            //If the currently held item is the same one we stored as potential pickup, reset potential pickup.
            for (int i = 0; i < potentialPickups.Count; i++)
            {
                if (heldItem == potentialPickups[i])
                {
                    potentialPickups.Remove(potentialPickups[i]);
                }
            }
        }
        

        if (GameManager.instance.curRoom == GameManager.Rooms.FARM)
        {
            if(!canPickup)  return;

            heldItem = item;
            

            if(heldItem.TryGetComponent(out MeshRenderer mRend))
            {
                matArray2 = matArray.ToList();

                if (matArray2.Count >= 2)
                {
                    matArray2.Remove(matArray2[1]);
                }

                matArray = matArray2.ToArray();

                mRend.materials = matArray;
            }
            
            //If item has a Collider and/or Rigidbody, disable them while held.
            if(heldItem.TryGetComponent(out Rigidbody itemRB))
            {
                itemRB.useGravity = false;
                itemRB.velocity = Vector3.zero;
            }

            if (heldItem.TryGetComponent(out Collider itemCollider))
            {
                itemCollider.enabled = false;
            }
            
            for (int i = 0; i < items.ItemDatabase.Count; i++)
            {
                if (item.name == items.ItemDatabase[i].name)
                {
                    for (int j = 0; j < PlayerLogic.instance.inventory.Count; j++)
                    {
                        if (PlayerLogic.instance.inventory[j].name == string.Empty)
                        {
                            PlayerLogic.instance.inventory[j] = items.ItemDatabase[i];
                            Destroy(potentialPickups[0]);
                            potentialPickups.Remove(potentialPickups[0]);
                            Invoke("ClearPickupsForNull", 0.1f);
                            if(PlayerLogic.instance.itemIndex == j)
                            {
                                HoldInventoryItem(PlayerLogic.instance
                                    .inventory[PlayerLogic.instance.itemIndex].model);
                            }
                            return;
                        }   
                    }
                }
            }
        }
    }

    public void ClearPickupsForNull()
    {
        for (int i = 0; i < potentialPickups.Count; i++)
        {
            if (potentialPickups[i] == null)
            {
                potentialPickups.Remove(potentialPickups[i]);
            }
        }
    }

    public void DropItem()
    {
        //If item has Collider and Rigidbody, re-enable them.
        if(heldItem.TryGetComponent(out Rigidbody heldItemRB))
        {
            heldItemRB.useGravity = true;
        }

        if (heldItem.TryGetComponent(out Collider heldItemCollider))
        {
            heldItemCollider.enabled = true;
        }
        
        //Reset variables.    
        heldItem.transform.parent = null;
        heldItem = null;
        holdingItem = false;
    }

    public void DropInventoryItem()
    {
        //If item has Collider and Rigidbody, re-enable them.
        if(inventoryItem.TryGetComponent(out Rigidbody invItemRB))
        {
            invItemRB.useGravity = true;
        }

        if (inventoryItem.TryGetComponent(out Collider invItemCollider))
        {
            invItemCollider.enabled = true;
        }
        
        //Reset variables.    
        inventoryItem.name = PlayerLogic.instance.inventory[PlayerLogic.instance.itemIndex].name;
        inventoryItem.transform.parent = null;
        inventoryItem = null;
        PlayerLogic.instance.inventory[PlayerLogic.instance.itemIndex] = PlayerLogic.instance.empty;
        holdingItem = false;
        
        ClearPickupsForNull();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //If we are near an item that can be picked up, set it as potential pickup target.
        if (other.CompareTag("PickUp"))
        {
            potentialPickups.Add(other.gameObject);
            ClearPickupsForNull();

            if (other.TryGetComponent<MeshRenderer>(out MeshRenderer mRend))
            {
                matArray = mRend.materials;

                matArray2 = matArray.ToList();
        
                if (matArray2.Count == 1)
                {
                    matArray2.Add(outlineShader);

                    matArray = matArray2.ToArray();

                    mRend.materials = matArray;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If far away from potential item to pickup, remove it as a pickup target.
        if (other.CompareTag("PickUp"))
        {
            if (other.TryGetComponent<MeshRenderer>(out MeshRenderer mRend))
            {
                matArray = mRend.materials;
                
                matArray2 = matArray.ToList();
            
                if (matArray2.Count == 2)
                {
                    matArray2.Remove(matArray2[1]);

                    matArray = matArray2.ToArray();

                    mRend.materials = matArray;
                }
            }
            potentialPickups.Remove(other.gameObject);
            ClearPickupsForNull();
        }
    }
    
    public void HoldInventoryItem(GameObject item)
    {
        if (heldItem != item)
        {
            if (inventoryItem)
            {
                Destroy(inventoryItem);
            }
            heldItem = item;
            inventoryItem = Instantiate(item, transform);
        }
        else
        {
            heldItem = null;
        }
        
        //If item has a Collider and/or Rigidbody, disable them while held.
        if(heldItem.TryGetComponent(out Rigidbody itemRB))
        {
            itemRB.useGravity = false;
            itemRB.velocity = Vector3.zero;
        }

        if (heldItem.TryGetComponent(out Collider itemCollider))
        {
            itemCollider.enabled = false;
        }
        
        ClearPickupsForNull();
    }

    private void Update()
    {
        if (GameManager.instance.curRoom == GameManager.Rooms.INTRO)
        {
            //Pick up the potential pick up target.
            if (Input.GetKeyUp(KeyCode.E))
            {
                if(potentialPickups.Count >= 1) {PickupItem(potentialPickups[0]);}
            }
        
            //Drop currently held item.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (holdingItem)
                {
                    DropItem();
                }
            }
        }

        if (GameManager.instance.curRoom == GameManager.Rooms.FARM)
        {
            //Pick up the potential pick up target.
            if (Input.GetKeyUp(KeyCode.E))
            {
                if(potentialPickups.Count >= 1) {PickupItem(potentialPickups[0]);}
            }
        
            //Drop currently held item.
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (inventoryItem != null)
                {
                    DropInventoryItem();
                }
            }
        }
    }
}
