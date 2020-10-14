using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
[RequireComponent(typeof(MeleeEnemyController))]
public class MonsterHealthMelee : MonsterHealth
{
    //public GameObject gold;//黃裝
    //public GameObject white;//白裝
    //public GameObject blue;//綠裝
    ////public GameObject GreenHelmet;
    //public GameObject GreenHead; //綠裝/頭盔
    //public GameObject GreenBreastplate;//綠裝/胸甲
    //public GameObject GreenLeg;//綠裝/腿甲
    //public GameObject SkillBookWhite;
    //public GameObject SkillBookGold;
    //public GameObject SkillBookBlue;
    MeleeEnemyController meleeEnemyController;

    public override void MonsterDead()
    {
        meleeEnemyController = GetComponent<MeleeEnemyController>();
        //避免怪物死亡後還繼續追著玩家
        meleeEnemyController.enabled = false;
        base.MonsterDead();
    }
}
