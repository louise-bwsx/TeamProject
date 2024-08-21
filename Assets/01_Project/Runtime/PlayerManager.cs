using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [SerializeField] private PlayerStats playerPrefab;
    [field: SerializeField, ReadOnly] public PlayerStats Player { get; private set; }
    [field: SerializeField, ReadOnly] public PlayerControl PlayerControl { get; private set; }
    [field: SerializeField, ReadOnly] public SkillSelector SkillSelector { get; private set; }
    [field: SerializeField, ReadOnly] public SkillShooter SkillShooter { get; private set; }
    [field: SerializeField, ReadOnly] public PlayerStamina PlayerStamina { get; private set; }
    [field: SerializeField, ReadOnly] public ShootDirectionSetter ShootDirection { get; private set; }
    [field: SerializeField, ReadOnly] public RemoteSkillPosition RemoteSkillPosition { get; private set; }

    public void SpawnPlayer()
    {
        GameSaveData gameSave = SaveManager.Inst.GetGameSave();
        Player = Instantiate(playerPrefab);
        Player.transform.rotation = Quaternion.Euler(0, -90, 0);
        if (gameSave.GetPosition() == Vector3.zero)
        {
            Player.transform.position = new Vector3(107.5178f, 14.23846f, 72.25961f);
            Player.transform.position = new Vector3(112.5f, 10.7f, 49.5f);
        }
        else
        {
            Player.transform.position = gameSave.GetPosition();
        }
        Player.name = Player.name.Replace("(Clone)", "");
        PlayerControl = Player.GetComponent<PlayerControl>();
        SkillSelector = Player.GetComponent<SkillSelector>();
        SkillShooter = Player.GetComponent<SkillShooter>();
        PlayerStamina = Player.GetComponent<PlayerStamina>();
        ShootDirection = Player.GetComponent<ShootDirectionSetter>();
        RemoteSkillPosition = Player.GetComponent<RemoteSkillPosition>();
    }

    public bool IsDead()
    {
        if (!Player)
        {
            return false;
        }
        return Player.IsDead();
    }

    public bool IsAttacking()
    {
        if (!Player)
        {
            return false;
        }
        return PlayerControl.isAttack;
    }
}