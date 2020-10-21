using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    public GameObject brokenWheel;
    public Transform brokenPos;
    public bool[] bossUltTimes;
    public BossController bossController;
    public override void Update()
    {
        base.Update();
        if (Hp <= 0)
        {
            //Destroy(gameObject);
        }
    }

    public override void MonsterDead()
    {
        base.MonsterDead();
    }
    public override void OnTriggerEnter(Collider other)
    {
        //Boss血量第一階段扣血條件
        if (Hp > maxHp * 0.7)
        {
            base.OnTriggerEnter(other);
            if (Hp < maxHp * 0.7)
            {
                animator.SetTrigger("Wheel_1_Broke");
            }
        }
        //Boss血量第二階段 當雕像打爆以後會繞過碰觸機制直接GetHit Boss
        else if (Hp < maxHp * 0.7 && Hp>maxHp*0.3)
        {
            base.OnTriggerEnter(other);
            //只有組合技才能造成傷害
            //if (other.CompareTag("爆炸"))
            //{
            //}
            //else if (other.CompareTag("火龍捲"))
            //{ 
            //}
            if (Hp < maxHp * 0.3)
            {
                animator.SetTrigger("Wheel_2_Broke");
            }
        }
        //Boss血量第三階段此時Boss開始會放大招
        else if (Hp < maxHp * 0.3)
        {
            base.OnTriggerEnter(other);
            if (Hp < 0)
            {
                GameObject FX = Instantiate(brokenWheel, brokenPos.position, transform.rotation);
                Destroy(FX, 2.5f);
            }
        }
        //if (Hp < maxHp * 0.35 && bossUltTimes[0] == false)
        //{
        //    bossController.BossUltAttack();
        //    bossController.bossUltTimes = 0;
        //    bossUltTimes[0] = true;
        //}
        //else if (Hp < maxHp * 0.25 && bossUltTimes[1] == false)
        //{
        //    bossController.BossUltAttack();
        //    bossController.bossUltTimes = 0;
        //    bossUltTimes[1] = true;
        //}
        //else if (Hp < maxHp * 0.20 && bossUltTimes[2] == false)
        //{
        //    bossController.BossUltAttack();
        //    bossController.bossUltTimes = 0;
        //    bossUltTimes[2] = true;
        //}
        //else if (Hp < maxHp * 0.10 && bossUltTimes[3] == false)
        //{
        //    bossController.BossUltAttack();
        //    bossController.bossUltTimes = 0;
        //    bossUltTimes[3] = true;
        //}
    }
}