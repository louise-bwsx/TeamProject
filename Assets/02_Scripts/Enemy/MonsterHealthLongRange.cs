using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class MonsterHealthLongRange : MonsterHealth
{
    public override void MonsterDead()
    {
        enemyController.enabled = false;
        base.MonsterDead();
    }
}
