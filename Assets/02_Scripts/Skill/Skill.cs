using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public float staminaCost;
    public float destroyTime = 3F;
    public GameObject skillObject;
    public Transform skillPos;
    public Transform skillRotation;
    public MobileStats mobileStats;
    public float skillForce = 200f;
    public float skillTimer;//最後射擊時間
    public float skillCD;//射擊間隔
    public Image fillImage;
    void Start()
    {
        skillTimer = 10f;//確保一開始都能按技能
        mobileStats = FindObjectOfType<MobileStats>();
    }
    void Update()
    {
        skillTimer += Time.deltaTime;
        if (skillTimer < skillCD)
        {
            fillImage.fillAmount = (skillCD - skillTimer) / skillCD;
        }
        else if (skillTimer >= skillCD)
        {
            fillImage.fillAmount = 0;
        }
    }
    public virtual void Shoot()
    {
        if (skillTimer > skillCD)
        { 
            GameObject bulletObj = Instantiate(skillObject);
            if (bulletObj != null)
            {
                bulletObj.transform.position = skillPos.position;
                bulletObj.transform.rotation = skillRotation.rotation;
                Rigidbody BulletObjRigidbody_ = bulletObj.GetComponent<Rigidbody>();
                if (BulletObjRigidbody_ != null)
                {
                    BulletObjRigidbody_.AddForce(bulletObj.transform.forward * skillForce);
                }
                skillTimer = 0;
                Destroy(bulletObj, destroyTime);
                mobileStats.stamina -= staminaCost;
                //射擊特效
                //扣能量
            }
        }
    }
}
