using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ChoppableTree : MonoBehaviour
{
    public bool playerInRange;
    public bool canBeChopped;

    public float treemaxHealth;
    public float treeHealth;

    public Animator animator;

    public float caloriesSpentChoppingWood=20;

    private void Start()
    {
        treeHealth = treemaxHealth;
        animator=transform.parent.transform.parent.GetComponent<Animator>();

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


    public void GetHit()
    {
        animator.SetTrigger("shake");

        treeHealth -= 1;
        PlayerState.Instance.currentCalories -= caloriesSpentChoppingWood;
        if (treeHealth <= 0)
        {
            treeIsDead();
        }   
    }



       

        void treeIsDead()
        {
            Vector3 treePosition = transform.position;
            Destroy(transform.parent.transform.parent.gameObject);
            canBeChopped = false;
            SelectionManager.instance.selectedTree = null;
            SelectionManager.instance.chopHolder.gameObject.SetActive(false);
            GameObject brokenTree = Instantiate(Resources.Load<GameObject>("ChoppedTree"), treePosition, Quaternion.Euler(0, 0, 0));

        }
   

    private void Update()
    {
        if (canBeChopped)
        {
            GlobalState.Instance.resourceHealth = treeHealth;
            GlobalState.Instance.resourceMaxHealth = treemaxHealth;
        }
    }
}
