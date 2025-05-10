using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    public GameObject ItemInfoUI;
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;

    public bool isOpen;

   // public bool isFull;

    public List<GameObject> SlotList = new List<GameObject>();
     
    public List <string> itemList = new List<string>();

    private GameObject itemToAdd;

    private GameObject whatSlotToEquip;
    public GameObject pickupAlert;
    public Text pickupName;
    public Image pickupImage;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
        populateSlotList();
        StartCoroutine(RemovePickupAlert());
        Cursor.visible=false;

    }

    private IEnumerator RemovePickupAlert()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.5f);
            pickupAlert.SetActive(false);
        }
    }

    private void populateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
           if(child.CompareTag ("Slot"))
            {
                SlotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {




        if (Input.GetKeyDown(KeyCode.I) && !isOpen&&!ConstructionManager.Instance.inConstructionMode)
        {
            inventoryScreenUI.SetActive(true);
            isOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if(!CraftingSystem.instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
            Cursor.visible = false;
            SelectionManager.instance.EnableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;
        }
    }
    public void AddToInventory(string itemName)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.pickupItemSound);
        GameObject availableSlot = FindNextEmptySlot();
        whatSlotToEquip = availableSlot;
            itemToAdd=Instantiate(Resources.Load<GameObject>(itemName),whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);
            itemList.Add(itemName);

        itemList.Add(itemName);
        TriggerPickupPopUp(itemName, itemToAdd.GetComponent<Image>().sprite);


        ReCalculateList();
        CraftingSystem.instance.RefreshNeededItems();

    }



    void TriggerPickupPopUp(string itemName, Sprite itemSprite)
    {
        pickupAlert.SetActive(true);
        pickupName.text = itemName;
        pickupImage.sprite = itemSprite;
        RemovePickupAlert();


    }

    private GameObject FindNextEmptySlot()
    {
    foreach(GameObject slot in SlotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckSlotsAvailable(int emptyNeeded)
    {
        int emptySlot = 0;
        foreach (GameObject slot in SlotList)
        {
            if (slot.transform.childCount <=0)
            {
                emptySlot+=1;
            }
           
        }
        if (emptySlot >=emptyNeeded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;
        for (var i = SlotList.Count-1;i>=0;i--)
        {
            if(SlotList[i].transform.childCount>0)
            {
                if (SlotList[i].transform.GetChild(0).name==nameToRemove+"(Clone)"&&counter!=0)
                {
                     Destroy(SlotList[i].transform.GetChild(0).gameObject);
                    counter -= 1;
                }
            }
        }
        ReCalculateList();
        CraftingSystem.instance.RefreshNeededItems();
    }
    public void ReCalculateList()
    {
    itemList.Clear();
        foreach (GameObject slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name = slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result=name.Replace(str2, "");


                itemList.Add(result);
            }
        }
    }
}
