using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF5Launch : Skill
{
    void Update()
    {
        lastFireTime += Time.deltaTime;
    }
    public void Shoot()
    {
        if (lastFireTime > fireRate)
        {
            GameObject bulletObj = Instantiate(skillObject);
            if (bulletObj != null)
            {
                bulletObj.transform.position = skillPos.position + skillPos.up;
                bulletObj.transform.rotation = skillRotation.rotation;
                Rigidbody BulletObjRigidbody_ = bulletObj.GetComponent<Rigidbody>();
                if (BulletObjRigidbody_ != null)
                {
                    BulletObjRigidbody_.AddForce(bulletObj.transform.forward * skillForce);
                }
                lastFireTime = 0;
                Destroy(bulletObj, destroyTime);
                playerControl.stamina -= staminaCost;
                //射擊特效
                //扣能量
            }
        }
    }
}
