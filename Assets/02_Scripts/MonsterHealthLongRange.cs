using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
[RequireComponent(typeof(LongRangeEnemyController))]
public class MonsterHealthLongRange : MonsterHealth
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
    LongRangeEnemyController longRangeEnemyController;

    void Start()
    {
        longRangeEnemyController = GetComponent<LongRangeEnemyController>();
    }
    public override void MonsterDead()
    {
        longRangeEnemyController.enabled = false;
        base.MonsterDead();
    }
}
