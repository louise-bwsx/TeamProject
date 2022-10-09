using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MonsterHealthMelee : MonsterHealth
{
    public override void MonsterDead()
    {
        //避免怪物死亡後還繼續追著玩家
        enemyController.enabled = false;
        base.MonsterDead();
    }
}
