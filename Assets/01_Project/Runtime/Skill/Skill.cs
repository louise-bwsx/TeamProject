using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class Skill : MonoBehaviour
{
    [SerializeField] protected PlayerStamina stamina;
    //把skillPos能貼合著地板
    [SerializeField] protected Transform skillPos;
    //把skillRotation能跟著地板一起轉
    [SerializeField] protected Transform skillRotation;
    [SerializeField] protected Image fillImage;
    [SerializeField] protected string prefabName;
    [SerializeField] protected float staminaCost;
    [SerializeField] protected float destroyTime;
    [SerializeField] protected float skillCD;//最後射擊時間
    [SerializeField] protected float skillRate;//射擊間隔

    private float skillForce = 500f;

    public virtual void Shoot()
    {
        Vector3 spawanPos = skillPos.position + skillPos.up * 0.7f;
        Quaternion spawnRotation = skillRotation.rotation;
        GameObject skillObject = ObjectPool.Inst.SpawnFromPool(prefabName, spawanPos, spawnRotation, duration: destroyTime);
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