using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarOnGame : MonoBehaviour
{

    public Slider slider;
    //public Gradient gradient;
    //public Image Fill;

    public void SetMaxHealth(float maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
        //Fill.color = gradient.Evaluate(1F);
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        //Fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
