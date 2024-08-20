using UnityEngine;
using UnityEngine.UI;

public class HealthBarOnGame : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(float maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }
}