using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    Poison,
    Water,
    Wind,
    Earth,
    Fire
}

public class Skill : MonoBehaviour
{
    [SerializeField] protected PlayerStamina stamina;
    //[SerializeField] protected Rigidbody skillObject;
    [SerializeField] protected Transform skillPos;
    [SerializeField] protected Transform skillRotation;
    [SerializeField] protected Image fillImage;
    [SerializeField] protected string prefabName;
    [SerializeField] protected float staminaCost;
    [SerializeField] protected float destroyTime;
    [SerializeField] protected float skillCD;//最後射擊時間
    [SerializeField] protected float skillRate;//射擊間隔

    private float skillForce = 500f;
    //skillCD <= 0 才能施放

    public virtual void Shoot()
    {
        Vector3 spawanPos = skillPos.position + skillPos.up * 0.7f;
        Quaternion spawnRotation = skillRotation.rotation;
        GameObject skillObject = ObjectPool.Inst.SpawnFromPool(prefabName, spawanPos, spawnRotation);
        if (skillObject == null)
        {
            return;
        }
        if (skillObject.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.AddForce(rigidbody.transform.forward * skillForce);
        }
        StartCoroutine(StartCoolDown());
        stamina.Cost(staminaCost);
    }

    public bool CanShoot()
    {
        return skillCD < skillRate;
    }

    protected IEnumerator StartCoolDown()
    {
        skillCD = 0;
        while (true)
        {
            skillCD -= Time.deltaTime;
            yield return null;
            if (skillCD < 0)
            {
                fillImage.fillAmount = 0;
                break;
            }
            fillImage.fillAmount = skillCD / skillRate;
        }
    }
}