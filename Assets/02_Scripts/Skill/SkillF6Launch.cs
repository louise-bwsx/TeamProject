using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF6Launch : Skill
{
    void Update()
    {
        lastFireTime += Time.deltaTime;
    }
    public void Shoot()
    {
        if (lastFireTime > fireRate)
        {
            GameObject BulletObj_ = Instantiate(skillObject);
            if (BulletObj_ != null)
            {
                BulletObj_.transform.position = skillPos.position + skillPos.up + skillPos.forward;
                BulletObj_.transform.rotation = skillRotation.rotation;
                Rigidbody BulletObjRigidbody_ = BulletObj_.GetComponent<Rigidbody>();
                if (BulletObjRigidbody_ != null)
                {
                    BulletObjRigidbody_.AddForce(BulletObj_.transform.forward * skillForce);
                }
            }
        }
    }
}
