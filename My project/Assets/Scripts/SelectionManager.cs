using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance{ get;set; }

    public GameObject interaction_Info_UI;
    public Text interaction_text;
    public GameObject selectedObject;

    public bool InTarget;
    public Image centerDotImage;
    public Image hanndIcon;

    public bool handIsVisible;

    public GameObject selectedTree;
    public GameObject chopHolder;




    private void Awake()
    {
        if(instance!=null&&instance!=this)
        {
Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }





    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject Interactable = selectionTransform.GetComponent<InteractableObject>();

            ChoppableTree choppableTree = selectionTransform.GetComponent<ChoppableTree>();
            if(choppableTree&&choppableTree.playerInRange)
            {
                choppableTree.canBeChopped = true;
                selectedTree = choppableTree.gameObject;
                chopHolder.gameObject.SetActive(true);
            }
            else
            {
                if(selectedTree != null)
                {
                    selectedTree.gameObject.GetComponent<ChoppableTree>().canBeChopped = false;
                    selectedTree = null;
                    chopHolder.gameObject.SetActive(false);
                }
            }


            if (Interactable && Interactable.playerInRange)
            {
                InTarget = true;
                selectedObject = Interactable.gameObject;
                interaction_text.text = Interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
                if (Interactable.CompareTag("Pickable"))
                {
                    centerDotImage.gameObject.SetActive(false);
                    hanndIcon.gameObject.SetActive(true);
                    handIsVisible = true;
                }
                else
                {
                    centerDotImage.gameObject.SetActive(true);
                    hanndIcon.gameObject.SetActive(false);
                    handIsVisible = false;
                }


            }
            else
            {
                InTarget = false;
                interaction_Info_UI.SetActive(false);
                centerDotImage.gameObject.SetActive(true);
                hanndIcon.gameObject.SetActive(false);

                handIsVisible = false;
            }     
        }
        else
        {
            InTarget = false;
            interaction_Info_UI.SetActive(false);
            centerDotImage.gameObject.SetActive(true);
            hanndIcon.gameObject.SetActive(false);

            handIsVisible = false;
        }
    }
    public void DisableSelection()
    {
        interaction_Info_UI.SetActive(false);
        centerDotImage.enabled = false;
        hanndIcon.enabled = false;
        selectedObject = null;
    }
    public void EnableSelection()
    {
        interaction_Info_UI.SetActive(true);
        centerDotImage.enabled = true;
        hanndIcon.enabled = true;
    }



}
