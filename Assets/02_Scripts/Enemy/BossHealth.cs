using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    public Transform minionsGroup;
    public override void MonsterDead()
    {
        base.MonsterDead();
    }
    public override void OnTriggerEnter(Collider other)
    {
        //Boss血量第一階段扣血條件
        if (Hp > maxHp * 0.7 && minionsGroup.childCount == 0)
        {
            base.OnTriggerEnter(other);
        }
        //Boss血量第二階段 當雕像打爆以後會繞過碰觸機制直接GetHit Boss

        //Boss血量第三階段此時Boss開始會放大招
        if (Hp < maxHp * 0.3)
        {
            base.OnTriggerEnter(other);
        }
    }
}