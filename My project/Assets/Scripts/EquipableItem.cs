using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EquipableItem:MonoBehaviour
{
  public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&InventorySystem.Instance.isOpen==false&&CraftingSystem.instance.isOpen==false&&SelectionManager.instance.handIsVisible==false&&!ConstructionManager.Instance.inConstructionMode)
        {
            StartCoroutine(SwingsoundDelay());
            animator.SetTrigger("hit");
        }
    }


    public void GetHit()
    {
        GameObject selectedTree = SelectionManager.instance.selectedTree;
        if (selectedTree != null)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.chopSound);
            selectedTree.GetComponent<ChoppableTree>().GetHit();
        }
    }
    IEnumerator SwingsoundDelay()
    {
        yield return new WaitForSeconds(0.2f);
        SoundManager.Instance.PlaySound(SoundManager.Instance.toolSwingSound);
    }
}
