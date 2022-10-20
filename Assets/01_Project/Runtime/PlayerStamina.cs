using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private UIBarControl uIBarControl;

    [SerializeField] private float stamina;
    public float Stamina
    {
        get { return stamina; }
        private set
        {
            stamina = value;
            uIBarControl.SetStamina(staminaPercentage);
        }
    }
    private float maxStamina = 100;
    private float staminaPercentage => Stamina / maxStamina;

    private void Start()
    {
        Stamina = maxStamina;
    }

    private void Update()
    {
        if (Stamina < maxStamina)
        {
            Stamina += Time.deltaTime * 10;
            if (Stamina > maxStamina)
            {
                Stamina = maxStamina;
            }
        }
    }

    public bool Enough(float cost)
    {
        return Stamina >= cost;
    }

    public void Cost(float cost)
    {
        Stamina -= cost;
    }
}