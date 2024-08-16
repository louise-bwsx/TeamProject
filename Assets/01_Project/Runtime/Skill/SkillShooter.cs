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

    Null,
    Count
}

public class SkillShooter : MonoBehaviour
{
    [SerializeField] private SkillSO[] skills;
    [SerializeField] private MeshRenderer shootDirectionMesh;

    //把remoteMesh能貼合著地板
    [SerializeField] private MeshRenderer remoteMesh;

    private LayerMask floor;
    private PlayerStamina stamina;
    private SkillSelector skillSelector;
    public UnityEvent<int> CastSkill = new UnityEvent<int>();

    private void Awake()
    {
        skillSelector = GetComponent<SkillSelector>();
        stamina = GetComponent<PlayerStamina>();
        floor = LayerMask.GetMask("Floor");
    }

    //TODO Magic_Prepare時還可以選方向 放開就Magic_Shoot
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
        Debug.Log("CastDirectionBasedSkill");
        Vector3 spawanPos = transform.position.With(y: transform.position.y + 0.7f);
        Debug.Log(spawanPos);
        Quaternion spawnRotation = shootDirectionMesh.transform.rotation;
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
        Debug.Log("CastPositionBasedSkill");
        float raylength = 500;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 spawanPos = remoteMesh.transform.position + remoteMesh.transform.up * 0.7f;
        GameObject skillObject = ObjectPool.Inst.SpawnFromPool(skillSO.prefabName,
                                                               spawanPos,
                                                               Quaternion.identity,
                                                               duration: skillSO.destroyTime);
        if (skillObject == null)
        {
            return;
        }
        if (Physics.Raycast(ray, out hit, raylength, floor))
        {
            remoteMesh.enabled = true;
            remoteMesh.transform.position = hit.point;
            skillObject.transform.position = hit.point;
        }
        StartCoroutine(skillSO.StartCoolDown());
        stamina.Cost(skillSO.staminaCost);
    }
}