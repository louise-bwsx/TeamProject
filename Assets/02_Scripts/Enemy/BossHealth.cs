using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    public GameObject brokenWheel;
    public Transform brokenPos;
    public GameObject bossDieDialogComponent;
    public GameObject ManiMenu;
    public float destroyTime = 2.5f;
    BossController bossController;
    public override void Start()
    {
        base.Start();
        bossController = FindObjectOfType<BossController>();
    }
    public override void Update()
    {
        base.Update();
        if (Hp <= 0)
        {
            destroyTime -= Time.deltaTime;
        }
        if (destroyTime <= 0)
        {
            bossDieDialogComponent.SetActive(true);
        }
    }

    public override void MonsterDead()
    {
        GameObject FX = Instantiate(brokenWheel, brokenPos.position, transform.rotation);
        Destroy(FX, destroyTime);
        base.MonsterDead();
    }
    public override void OnTriggerEnter(Collider other)
    {
        //Boss血量第一階段扣血條件
        if (Hp > maxHp * 0.7)
        {
            base.OnTriggerEnter(other);
            if (Hp <= maxHp * 0.7)
            {
                animator.SetTrigger("Wheel_1_Broke");
            }
        }
        //Boss血量第二階段 當雕像打爆以後會繞過碰觸機制直接GetHit Boss
        else if (Hp <= maxHp * 0.7 && Hp>maxHp*0.3)
        {
            base.OnTriggerEnter(other);
            //只有組合技才能造成傷害
            //if (other.CompareTag("爆炸"))
            //{
            //}
            //else if (other.CompareTag("火龍捲"))
            //{ 
            //}
            if (Hp <= maxHp * 0.3)
            {
                animator.SetTrigger("Wheel_2_Broke");
                bossController.BossUltAttack();
            }
        }
        //Boss血量第三階段此時Boss開始會放大招
        else if (Hp <= maxHp * 0.3)
        {
            base.OnTriggerEnter(other);
        }
    }
}