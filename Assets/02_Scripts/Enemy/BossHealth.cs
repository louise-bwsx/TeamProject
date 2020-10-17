using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    public override void MonsterDead()
    {
        base.MonsterDead();
    }
    public override void OnTriggerEnter(Collider other)
    {
        //當boss血量在maxHp時才能用正常手段攻擊
        if (Hp > maxHp * 0.7)
        { 
            base.OnTriggerEnter(other);
        }
    }
}