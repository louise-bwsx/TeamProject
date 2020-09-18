using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //public GetHitEffect getHitEffect;
    public float maxHealth = 100f;
    public float currentHealth { get; private set; }
    public Stats Damage;
    public Stats Armor;

    void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float Damage)
    {
        Damage -= Armor.GetValue();
        Damage = Mathf.Clamp(Damage, 0, float.MaxValue);

        currentHealth -= Damage;
        Debug.Log(transform.name + "被攻擊造成 " + Damage + " 傷害,剩餘 " + currentHealth + " 點生命");
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Debug.Log(transform.name + " 被擊敗");
    }
}
