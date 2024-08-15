using UnityEngine;
using UnityEngine.UI;

public class UIBarControl : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Image staminaFill;

    public void Init(PlayerStamina playerStamina)
    {
        playerStamina.OnStaminaChange.AddListener(SetStamina);
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

    public void SetHealth(float healthPercent)
    {
        healthFill.fillAmount = healthPercent;
    }

    public void SetStamina(float staminaPercent)
    {
        staminaFill.fillAmount = staminaPercent;
    }
}