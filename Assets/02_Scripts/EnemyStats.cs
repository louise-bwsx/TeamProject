using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public override void Die()
    {
        base.Die();
        //讓他變成含有物理效果的紙娃娃
        //死亡動畫
        Destroy(gameObject);
    }
}
