using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ResourceHealthBar : MonoBehaviour
{
    private Slider slider;
    private float maxHealth, currentHealth;
    public GameObject GlobalState;


    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        currentHealth = GlobalState.GetComponent<GlobalState>().resourceHealth;
        maxHealth = GlobalState.GetComponent<GlobalState>().resourceMaxHealth;
        float fillValue = currentHealth / maxHealth;
        slider.value = fillValue;
    }
}
  
