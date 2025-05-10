using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

    public float currentHealth;
    public float maxHealth;

    public float currentCalories;
    public float maxCalories;
    float distanceTraveled=0;
    Vector3 lastPosition;

    public GameObject playerBody;

    public float currentHydrationPercent;
    public float maxHydrationPercent;
    public bool isHydrationActive;



    public void setHealth(float health)
    {
        currentHealth = health;
    }

    public void setCalories(float calories)
    {
        currentCalories = calories;
    }
    public void setHydration(float hydartion)
    {
        currentHydrationPercent = hydartion;
    }


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
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;

        StartCoroutine(decreaseHydration());
    }

    IEnumerator decreaseHydration()
    {
    while(true)
        {
            currentHydrationPercent -= 1;
            yield return new WaitForSeconds(15);
          
        }
    }

    void Update()
    {
        distanceTraveled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;

        if (distanceTraveled >= 5)
        {
            currentCalories -= 1;
            distanceTraveled = 0;
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
    }
}
