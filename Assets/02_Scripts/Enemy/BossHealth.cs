using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    BossController bossController;
    public override void MonsterDead()
    {
        bossController = GetComponent<BossController>();
        bossController.enabled = false;
        base.MonsterDead();
    }
}
