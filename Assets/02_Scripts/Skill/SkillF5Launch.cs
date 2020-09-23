using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF5Launch : MonoBehaviour
{
    public GameObject SkillObject;
    public Transform SkillPos;
    public float SkillForce = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Skill()
    {
        GameObject BulletObj_ = Instantiate(SkillObject);
        if (BulletObj_ != null)
        {
            BulletObj_.transform.position = SkillPos.position;
            BulletObj_.transform.rotation = SkillPos.rotation;
            Rigidbody BulletObjRigidbody_ = BulletObj_.GetComponent<Rigidbody>();
            if (BulletObjRigidbody_ != null)
            {
                BulletObjRigidbody_.AddForce(BulletObj_.transform.forward * SkillForce);
            }
        }
    }
}
