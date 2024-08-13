using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarControl : MonoBehaviour
{
    public Slider healthSlider;
    public Slider staminaSlider;

    public void SetMaxHealth(float maxHp)
    {
        healthSlider.maxValue = maxHp;
        healthSlider.value = maxHp;
    }
    public void SetHealth(float playerHealth)
    {
        healthSlider.value = playerHealth;
    }
    public void SetMaxStamina(float maxStamina)
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }
    public void SetStamina(float stamina)
    {
        staminaSlider.value = stamina;
    }
}
