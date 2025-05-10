using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Data.SqlTypes;
using System.Collections;
using JetBrains.Annotations;


public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem instance { get; set; }
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI, survivalScreenUI, refineScreenUI,constructionScreenUI;
    public List<string> inventoryItemList = new List<string>();
    public Button toolsBTN, survivalButton, refineButton,constructionButton;
    public Button craftAxeButton, craftPlankButton,craftFoundation,craftWall;
    public GameObject CraftAxeButton;
    Text AxeReq1, AxeReq2, PlankReq1,FoundationReq1,WallReq1;
    public bool isOpen = false;
    public Blueprint AxeBLP = new Blueprint("Axe",1,2,"Stone",3,"Stick",3);
    public Blueprint PlankBLP = new Blueprint("Plank",2, 1, "Log", 1, "", 0);
    public Blueprint FoundationBLP = new Blueprint("Foundation", 1, 1, "Plank", 4, "", 0);
    public Blueprint WallBLP = new Blueprint("Wall", 1, 1, "Plank", 3, "", 0);
    

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        isOpen = false;
        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        survivalButton = craftingScreenUI.transform.Find("SurvivalButton").GetComponent<Button>();
        survivalButton.onClick.AddListener(delegate { OpenSurvivalCategory(); });

        refineButton = craftingScreenUI.transform.Find("RefineButton").GetComponent<Button>();
        refineButton.onClick.AddListener(delegate { OpenRefineCategory(); });

        constructionButton = craftingScreenUI.transform.Find("ConstructionButton").GetComponent<Button>();
        constructionButton.onClick.AddListener(delegate { OpenConstructionCategory(); });




        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();
        craftAxeButton = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeButton.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });

        //Plank
        PlankReq1 = refineScreenUI.transform.Find("Plank").transform.Find("req1").GetComponent<Text>();
        craftPlankButton = refineScreenUI.transform.Find("Plank").transform.Find("Button").GetComponent<Button>();
        craftPlankButton.onClick.AddListener(delegate { CraftAnyItem(PlankBLP); });

        //Foundation
        FoundationReq1 = constructionScreenUI.transform.Find("Foundation").transform.Find("req1").GetComponent<Text>();
        craftFoundation = constructionScreenUI.transform.Find("Foundation").transform.Find("Button").GetComponent<Button>();
        craftFoundation.onClick.AddListener(delegate { CraftAnyItem(FoundationBLP); });

        //Wall
        WallReq1 = constructionScreenUI.transform.Find("Wall").transform.Find("req1").GetComponent<Text>();
        craftWall = constructionScreenUI.transform.Find("Wall").transform.Find("Button").GetComponent<Button>();
        craftWall.onClick.AddListener(delegate { CraftAnyItem(WallBLP); });

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.C) && !isOpen&&!ConstructionManager.Instance.inConstructionMode)
        {
            craftingScreenUI.SetActive(true);
            isOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            refineScreenUI.SetActive(false);
            survivalScreenUI.SetActive(false);
            constructionScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            isOpen = false;
            Cursor.visible = false;
            SelectionManager.instance.EnableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;
        }
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
        constructionScreenUI.SetActive(false);
    }
    void OpenSurvivalCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        survivalScreenUI.SetActive(true);
        constructionScreenUI.SetActive(false);
    }
    void OpenRefineCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(true);
        constructionScreenUI.SetActive(false);

    }

    void OpenConstructionCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(false);
        survivalScreenUI.SetActive(false);
        refineScreenUI.SetActive(false);
        constructionScreenUI.SetActive(true);
    }
    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        StartCoroutine(craftSoundDelay(blueprintToCraft));
        
        
 
  

            
        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.Req1amount);
        }else if (blueprintToCraft.numOfRequirements ==2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.req2, blueprintToCraft.Req2amount);
        }
        StartCoroutine(calculate());


    }
    public IEnumerator craftSoundDelay(Blueprint blueprintToCraft)
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.craftItemSound);
        yield return new WaitForSeconds(1.3f);
       
        //produce the amount of items according to the blueprint
        for (var i = 0; i < blueprintToCraft.numberOfItensToProduce; i++)
        {
            InventorySystem.Instance.AddToInventory(blueprintToCraft.ItemName);
        }
    }
    public IEnumerator calculate()
    {
        yield return new WaitForSeconds(1f);
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }
    public void RefreshNeededItems()
    {
    int stone_count = 0;
    int stick_count = 0;
    int log_count = 0;
    int plank_count = 0;
        inventoryItemList = InventorySystem.Instance.itemList;
        foreach (string itemName in inventoryItemList)
        {
         switch(itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;
                case "Stick":
                    stick_count += 1;
                    break;
                case "Log":
                    log_count += 1;
                    break;
                case "Plank":
                    plank_count += 1;
                    break;
            }

        }

        //Axe
        AxeReq1.text="3 Stone["+ stone_count + "]";
        AxeReq2.text = "3 Stick[" + stick_count + "]";

        if(stone_count >= 3 && stick_count >= 3&&InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftAxeButton.interactable = true;
            CraftAxeButton.SetActive(true);

        }
        else
        {
            craftAxeButton.interactable = false;
            CraftAxeButton.SetActive(false);
        }

        //Plank
        PlankReq1.text = "1 Log[" + log_count + "]";

        if (log_count >= 1&&InventorySystem.Instance.CheckSlotsAvailable(2))
        {
           craftPlankButton.gameObject.SetActive(true);

        }
        else
        {
            craftPlankButton.gameObject.SetActive(false);
        }

        //Foundation
        FoundationReq1.text = "4 Plank[" + plank_count + "]";
        if (plank_count >= 4 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftFoundation.gameObject.SetActive(true);
        }
        else
        {
            craftFoundation.gameObject.SetActive(false);
        }

        //Wall
        WallReq1.text = "3 Plank[" + plank_count + "]";
        if (plank_count >= 3 && InventorySystem.Instance.CheckSlotsAvailable(1))
        {
            craftWall.gameObject.SetActive(true);
        }
        else
        {
            craftWall.gameObject.SetActive(false);
        }
    }
}
