using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; set; }
    public GameObject menuCanvas;
    public GameObject UICanvas;
    public bool isMenuOpen;

    public GameObject saveMenu;
    public GameObject settingsMenu;
    public GameObject menu;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)&&isMenuOpen==false)
        {
            UICanvas.SetActive(false);
            menuCanvas.SetActive(true);
            isMenuOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.M) && isMenuOpen == true)
        {
            saveMenu.SetActive(false);
            settingsMenu.SetActive(false);
            menu.SetActive(true);

            UICanvas.SetActive(true);
            menuCanvas.SetActive(false);
            isMenuOpen = false;
            if (CraftingSystem.instance.isOpen==false&&InventorySystem.Instance.isOpen==false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            SelectionManager.instance.EnableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;
        }
    }

}
