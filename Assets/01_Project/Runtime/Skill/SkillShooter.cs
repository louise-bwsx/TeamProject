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

    //把remoteMesh能貼合著地板
    [SerializeField] private MeshRenderer remoteMesh;

    private PlayerStamina stamina;
    private SkillSelector skillSelector;
    public UnityEvent<int> CastSkill = new UnityEvent<int>();

    private void Awake()
    {
        skillSelector = GetComponent<SkillSelector>();
        stamina = GetComponent<PlayerStamina>();
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
                CastSkill?.Invoke(skillSelector.CurrentIndex);
                break;
            //水火
            case (int)SkillType.Water:
            case (int)SkillType.Fire:
                CastDirectionBasedSkill(skills[skillSelector.CurrentIndex]);
                CastSkill?.Invoke(skillSelector.CurrentIndex);
                break;
            default:
                break;
        }
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
        StartCoroutine(skillSO.StartCoolDown());
        stamina.Cost(skillSO.staminaCost);
    }

    private void CastPositionBasedSkill(SkillSO skillSO)
    {
        //Debug.Log("CastPositionBasedSkill");
        Vector3 spawanPos = remoteMesh.transform.position;
        GameObject skillObject = ObjectPool.Inst.SpawnFromPool(skillSO.prefabName,
                                                               spawanPos,
                                                               Quaternion.identity,
                                                               duration: skillSO.destroyTime);
        if (skillObject == null)
        {
            return;
        }
        StartCoroutine(skillSO.StartCoolDown());
        stamina.Cost(skillSO.staminaCost);
    }
}