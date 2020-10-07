using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillF8Launch : MonoBehaviour
{
    public GameObject SkillObject;
    public Transform spawnPosition;
    public float lastFireTime;//最後射擊時間
    public float fireRate = 1f;//射擊間隔
    //public ParticleSystem muzzleVFX;//放置粒子物件
    public void shoot()//射擊方法
    {
        if (Time.time >= lastFireTime + fireRate)//目前時間>=最後射擊時間+間隔時間
        {
            FirOneShoot();//單發射擊
        }
    }
    public void Skill()
    {
        GameObject BulletObj = Instantiate(SkillObject);
        if (BulletObj != null)
        {
            BulletObj.transform.position = spawnPosition.position + spawnPosition.forward;
            BulletObj.transform.rotation = transform.rotation;
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
        //ShowShootVFX();
        //扣子彈

        //呼叫射線
    }
    //public void ShowShootVFX()
    //{
    //    if (muzzleVFX)
    //        muzzleVFX.Play();
    //}
}
