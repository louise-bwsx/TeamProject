using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF6Launch : Skill
{
    void Update()
    {
        lastFireTime += Time.deltaTime;
    }
    public override void Shoot()
    {
        if (lastFireTime > fireRate)
        {
            GameObject shootingSkill = Instantiate(skillObject);
            if (shootingSkill != null)
            {
                shootingSkill.transform.position = skillPos.position + skillPos.up + skillPos.forward;
                shootingSkill.transform.rotation = skillRotation.rotation;
                Rigidbody BulletObjRigidbody_ = shootingSkill.GetComponent<Rigidbody>();
                if (BulletObjRigidbody_ != null)
                {
                    BulletObjRigidbody_.AddForce(shootingSkill.transform.forward * skillForce);
                }
                lastFireTime = 0;
                Destroy(shootingSkill, destroyTime);
                playerControl.stamina -= staminaCost;
            }
        }
    }
}
