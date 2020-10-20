﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    public override void Update()
    {
        base.Update();
        if (Hp <= 0)
        {
            Destroy(gameObject);
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
        }
        //Boss血量第二階段 當雕像打爆以後會繞過碰觸機制直接GetHit Boss
        else if (Hp < maxHp * 0.7)
        {
            base.OnTriggerEnter(other);
            //只有組合技才能造成傷害
            //if (other.CompareTag("爆炸"))
            //{
            //}
            //else if (other.CompareTag("火龍捲"))
            //{ 
            //}
        }
        //Boss血量第三階段此時Boss開始會放大招
        else if (Hp < maxHp * 0.3)
        {
            base.OnTriggerEnter(other);
        }
    }
}