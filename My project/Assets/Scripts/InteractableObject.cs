using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;
    public bool playerInRange;

    public string GetItemName()
    {
        return ItemName;
    }
    private void Start()
    {
        playerInRange = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&playerInRange&&SelectionManager.instance.InTarget&&SelectionManager.instance.selectedObject==gameObject)
        {
            if(InventorySystem.Instance.CheckSlotsAvailable(1))
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            playerInRange = true;
       
        }
        else
        {
            playerInRange = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
        else
        {
            playerInRange = false;
        }
    }
}

