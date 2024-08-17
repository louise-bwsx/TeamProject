using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private PlayerControl playerControl;
    [SerializeField] private PlayerStats playerPrefab;
    [field: SerializeField, ReadOnly] public PlayerStats Player { get; private set; }
    [field: SerializeField, ReadOnly] public SkillSelector SkillSelector { get; private set; }
    [field: SerializeField, ReadOnly] public PlayerStamina PlayerStamina { get; private set; }

    public void SpawnPlayer()
    {
        Player = Instantiate(playerPrefab, new Vector3(112.5f, 10.7f, 49.5f), Quaternion.Euler(0, -90, 0));
        Player.name = Player.name.Replace("(Clone)", "");
        playerControl = Player.GetComponent<PlayerControl>();
        SkillSelector = Player.GetComponent<SkillSelector>();
        PlayerStamina = Player.GetComponent<PlayerStamina>();
    }

    public bool IsDead()
    {
        if (!Player)
        {
            return false;
        }
        return Player.hp <= 0;
    }

    public bool IsAttacking()
    {
        if (!Player)
        {
            return false;
        }
        return playerControl.isAttack;
    }
}