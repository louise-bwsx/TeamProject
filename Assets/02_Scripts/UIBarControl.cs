using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarControl : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient healthGradient;
    public Image healthFill;

    public Slider staminaSlider;
    public Gradient staminaGradient;
    public Image staminaFill;
    public GetHitEffect getHitEffect;
    public void SetMaxHealth(float maxHp)
    {
        healthSlider.maxValue = maxHp;
        healthSlider.value = maxHp;
    }
    public void SetHealth(float playerHealth)
    {
        healthSlider.value = playerHealth;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);//血條變色
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
