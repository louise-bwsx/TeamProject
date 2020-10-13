using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class BossHealthControl : MonsterHealth
{
    BossController bossController;
    void Start()
    {
        Hp = maxHp;
        healthBarOnGame.SetMaxHealth(maxHp);
        bossController = GetComponent<BossController>();
    }

    public override void MonsterDead()
    {
        base.MonsterDead();
    }
}
