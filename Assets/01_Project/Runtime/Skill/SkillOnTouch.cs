using UnityEngine;

public class SkillOnTouch : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectObject;
    [SerializeField] private GameObject advancedSkllObject;
    [SerializeField] private float advancedDestroyTime;
    [SerializeField] private string[] onTouchCastAdvancedSkillTags;
    [SerializeField] private string[] onTouchDisableTags;

    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    //為了讓重回pool再生成的skillcollider打開
    private void OnEnable()
    {
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" ||
            other.gameObject.tag == "Default"//為了不打到隱形牆
            )
        {
            return;
        }
        Debug.Log($"技能打到: {other.gameObject}");
        if (hitEffectObject)
        {
            SpawanHitEffect(other);
        }
        if (advancedSkllObject)
        {
            CastAdvancedSkill(other);
        }
        OnTouchDisable(other);
    }

    private void SpawanHitEffect(Collider other)
    {
        ObjectPool.Inst.SpawnFromPool(hitEffectObject.name, transform.position, transform.rotation, other.transform.parent, duration: 1f);
    }

    private void CastAdvancedSkill(Collider other)
    {
        for (int i = 0; i < onTouchCastAdvancedSkillTags.Length; i++)
        {
            if (other.CompareTag(onTouchCastAdvancedSkillTags[i]))
            {
                ObjectPool.Inst.SpawnFromPool(advancedSkllObject.name, transform.position, transform.rotation, duration: advancedDestroyTime);
                break;
            }
        }
    }

    private void OnTouchDisable(Collider other)
    {
        for (int i = 0; i < onTouchDisableTags.Length; i++)
        {
            if (other.CompareTag(onTouchDisableTags[i]))
            {
                Debug.Log(gameObject.name);
                Debug.Log(other.name);
                ObjectPool.Inst.PutBackInPool(gameObject, 0);
                break;
            }
        }
    }
}