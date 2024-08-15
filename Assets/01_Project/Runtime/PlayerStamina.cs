using UnityEngine;
using UnityEngine.Events;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float stamina;
    private float maxStamina = 100;
    public UnityEvent<float> OnStaminaChange = new UnityEvent<float>();

    private void Start()
    {
        stamina = maxStamina;
        OnStaminaChange?.Invoke(GetStaminaPercentage());
    }

    private void Update()
    {
        if (stamina < maxStamina)
        {
            stamina += Time.deltaTime * 10;
            if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
            OnStaminaChange?.Invoke(GetStaminaPercentage());
        }
    }

    public bool IsEnough(float cost)
    {
        return stamina >= cost;
    }

    public void Cost(float cost)
    {
        stamina -= cost;
        OnStaminaChange?.Invoke(GetStaminaPercentage());
    }

    private float GetStaminaPercentage()
    {
        //為了不除以0
        if (stamina == 0)
        {
            return 0.01f;
        }
        return stamina / maxStamina;
    }
}