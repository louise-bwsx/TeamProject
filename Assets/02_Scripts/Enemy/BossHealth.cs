using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossController))]
public class BossHealth : MonsterHealth
{
    BossController bossController;
    public void BossMeleeAttackAction()
    {
        animator.SetTrigger("WeaponAttackEnd");
    }
    public override void MonsterDead()
    {
        bossController = GetComponent<BossController>();
        bossController.enabled = false;
        base.MonsterDead();
    }
}
