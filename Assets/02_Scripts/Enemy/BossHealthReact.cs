using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthReact : MonoBehaviour
{
    BossHealth bossHealth;
    BossFightRule bossFightRule;
    public GameObject statue;
    public GameObject guardStatue;//為了不讓Wheel_1_重複呼叫
    public GameObject guardArea;

    bool wheel1Trigger = true;
    bool wheel2Trigger = true;//為了不讓Wheel_2_Broke重複呼叫
    void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        bossFightRule = GetComponent<BossFightRule>();
    }

    void Update()
    {
        if (bossFightRule.bossFightState ==2)
        {
            bossHealth.animator.SetTrigger("Wheel_1_Broke");
        }
        if (bossHealth.Hp < bossHealth.maxHp * 0.3 && wheel2Trigger)
        {
            //停止但不消失 可能重複呼叫
            bossHealth.animator.SetTrigger("Wheel_2_Broke");
            wheel2Trigger = false;
        }
    }
}
