using UnityEngine;
using UnityEngine.UI;

public class UIBarControl : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Image staminaFill;

    public void Init(PlayerStamina playerStamina, PlayerStats playerStats)
    {
        playerStamina.OnStaminaChange.AddListener(SetStamina);
        playerStats.OnHealthChange.AddListener(SetHealth);
    }

    public void OpenUI()
    {
        healthFill.gameObject.SetActive(true);
        staminaFill.gameObject.SetActive(true);
    }

    public void HideUI()
    {
        healthFill.gameObject.SetActive(false);
        staminaFill.gameObject.SetActive(false);
    }

    public void SetMaxHealth()
    {
        healthFill.fillAmount = 1;
    }

    public void SetMaxStamina()
    {
        staminaFill.fillAmount = 1;
    }

    private void SetHealth(float healthPercent)
    {
        healthFill.fillAmount = healthPercent;
    }

    private void SetStamina(float staminaPercent)
    {
        staminaFill.fillAmount = staminaPercent;
    }
}