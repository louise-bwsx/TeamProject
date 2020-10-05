using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF8Launch : MonoBehaviour
{
    public GameObject SkillObject;
    public Transform SkillPos;
    public float SkillForce = 200f;
    public float lastFireTime;//最後射擊時間
    public float fireRate = 1f;//射擊間隔
    public ParticleSystem muzzleVFX;//放置粒子物件
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shoot()//射擊方法
    {
        if (Time.time >= lastFireTime + fireRate)//目前時間>=最後射擊時間+間隔時間
        {

            FirOneShoot();//單發射擊
        }
        //else
        //{
        //    FailedShoot();//射擊延遲時間
        //}
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
    public void FirOneShoot()//單發射擊
    {
        lastFireTime = Time.time;//儲存目前時間
        Skill();
        //FirRaycast();//攻擊行為
        //射擊音效

        //射擊間隔

        //射擊特效
        ShowShootVFX();
        //扣子彈

        //呼叫射線
    }
    //public void FailedShoot()//射擊延遲時間
    //{
    //    lastFireTime = Time.time + 0.5f;//現在時間

    //}
    public void ShowShootVFX()
    {
        if (muzzleVFX)
            muzzleVFX.Play();
    }
}
