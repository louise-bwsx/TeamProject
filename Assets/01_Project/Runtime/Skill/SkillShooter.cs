using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum SkillType
{
    Poison,
    Water,
    Wind,
    Earth,
    Fire,

    FireTornado,
    Bomb,

    Null,
}

public class SkillShooter : MonoBehaviour
{
    [SerializeField] private SkillSO[] skills;
    [SerializeField] private Transform shootDirection;
    [SerializeField] private Transform directionMesh;
    [SerializeField] private Transform remoteMesh;
    private PlayerStamina stamina;
    private SkillSelector skillSelector;
    public UnityEvent<int> CooldownStart = new UnityEvent<int>();
    public UnityEvent<int, float, float> Cooldowning = new UnityEvent<int, float, float>();
    public UnityEvent<int> CooldownEnd = new UnityEvent<int>();

    private void Awake()
    {
        skillSelector = GetComponent<SkillSelector>();
        stamina = GetComponent<PlayerStamina>();
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < skills.Length - 1; i++)
        {
            skills[i].skillCD = 0;
        }
    }

    public void Cast()
    {
        switch (skillSelector.CurrentIndex)
        {
            //毒風土
            case (int)SkillType.Poison:
            case (int)SkillType.Wind:
            case (int)SkillType.Earth:
                CastPositionBasedSkill(skills[skillSelector.CurrentIndex]);
                break;
            //水火
            case (int)SkillType.Water:
            case (int)SkillType.Fire:
                CastDirectionBasedSkill(skills[skillSelector.CurrentIndex]);
                break;
            default:
                break;
        }
        directionMesh.gameObject.SetActive(false);
        remoteMesh.gameObject.SetActive(false);
    }

    private void CastDirectionBasedSkill(SkillSO skillSO)
    {
        //Debug.Log("CastDirectionBasedSkill");
        Vector3 spawanPos = transform.position.With(y: transform.position.y + 0.7f);
        //Debug.Log(spawanPos);
        Quaternion spawnRotation = shootDirection.transform.rotation;
        GameObject skillObject = ObjectPool.Inst.SpawnFromPool(skillSO.prefabName,
                                                               spawanPos,
                                                               spawnRotation,
                                                               duration: skillSO.destroyTime);
        if (skillObject == null)
        {
            return;
        }
        if (skillObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.velocity = rigidbody.transform.forward * skillSO.skillForce;
        }
        StartCoroutine(StartCoolDown(skillSO, skillSelector.CurrentIndex));
        stamina.Cost(skillSO.staminaCost);
    }

    private void CastPositionBasedSkill(SkillSO skillSO)
    {
        //Debug.Log("CastPositionBasedSkill");
        Vector3 spawanPos = remoteMesh.position;
        GameObject skillObject = ObjectPool.Inst.SpawnFromPool(skillSO.prefabName,
                                                               spawanPos,
                                                               Quaternion.identity,
                                                               duration: skillSO.destroyTime);
        if (skillObject == null)
        {
            return;
        }
        StartCoroutine(StartCoolDown(skillSO, skillSelector.CurrentIndex));
        stamina.Cost(skillSO.staminaCost);
    }

    public IEnumerator StartCoolDown(SkillSO skillSO, int index)
    {
        Debug.Log($"{skillSO.name} StartCoolDown");
        CooldownStart.Invoke(index);
        skillSO.skillCD = skillSO.skillRate;
        while (true)
        {
            //不要將 yield return null 移到判斷pause底下 會無限迴圈
            yield return null;
            if (!GameStateManager.Inst.IsGaming())
            {
                continue;
            }
            skillSO.skillCD -= Time.deltaTime;
            Cooldowning?.Invoke(index, skillSO.skillCD, skillSO.skillRate);
            if (skillSO.skillCD <= 0)
            {
                CooldownEnd.Invoke(index);
                break;
            }
        }
    }

    public void ShowAimMesh(int index)
    {
        switch (index)
        {
            case 0:
            case 2:
            case 3:
                remoteMesh.gameObject.SetActive(true);
                break;
            case 1:
            case 4:
                directionMesh.gameObject.SetActive(true);
                break;

        }
    }
}