using System.Collections;
using UnityEngine;

public class SkillOnTouch : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectObject;
    [SerializeField] private string[] hitEffectSpawnTags;
    [SerializeField] private GameObject advancedSkllObject;
    [SerializeField] private float advancedDestroyTime;
    [SerializeField] private string[] onTouchCastAdvancedSkillTags;
    [SerializeField] private string[] onTouchDisableTags;

    //TODO: 水技能的攻擊效果暫時放在這邊 找機會分離
    private float explodeRadius = 5f;
    private float force = 700f;
    private float explodeTimer = 1f;

    private void Start()
    {
        if (gameObject.name == "PlayerWaterSkill")
        {
            StartCoroutine(Explode(explodeTimer));
        }
    }

    private IEnumerator Explode(float explodeTimer)
    {
        if (explodeTimer > 0)
        {
            yield return new WaitForSeconds(explodeTimer);
        }
        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, explodeRadius);
            }
        }
        ObjectPool.Inst.PutBackInPool(gameObject, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
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
        for (int i = 0; i < hitEffectSpawnTags.Length; i++)
        {
            if (other.CompareTag(hitEffectSpawnTags[i]))
            {
                ObjectPool.Inst.SpawnFromPool(hitEffectObject.name, transform.position, transform.rotation, other.transform.parent, duration: 1f);
                break;
            }
            if (gameObject.CompareTag("WaterAttack"))
            {
                StartCoroutine(Explode(0));
            }
        }
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