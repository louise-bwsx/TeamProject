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
    [SerializeField] protected Rigidbody skillObject;
    [SerializeField] protected Transform skillPos;
    [SerializeField] protected Transform skillRotation;
    [SerializeField] protected Image fillImage;
    [SerializeField] protected float staminaCost;
    [SerializeField] protected float destroyTime;
    [SerializeField] protected float skillCD;//最後射擊時間
    [SerializeField] protected float skillRate;//射擊間隔

    private float skillForce = 500f;
    //skillCD <= 0 才能施放

    public virtual void Shoot()
    {
        Rigidbody bulletObj = Instantiate(skillObject);
        bulletObj.position = skillPos.position + skillPos.up * 0.7f;
        bulletObj.rotation = skillRotation.rotation;
        bulletObj.AddForce(bulletObj.transform.forward * skillForce);
        skillCD = 0;
        Destroy(bulletObj, destroyTime);
        StartCoroutine(CoolDown());
        stamina.Cost(staminaCost);
    }

    public bool CanShoot()
    {
        return skillCD < skillRate;
    }

    protected IEnumerator CoolDown()
    {
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