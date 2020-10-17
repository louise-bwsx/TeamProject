using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthReact : MonoBehaviour
{
    BossHealth bossHealth;
    public GameObject statue;
    public GameObject guardStatue;//為了不讓Wheel_1_重複呼叫
    public GameObject guardArea;
    bool Wheel2Trigger = true;//為了不讓Wheel_2_Broke重複呼叫
    void Start()
    {
        bossHealth = GetComponent<BossHealth>();    
    }

    void Update()
    {
        if (bossHealth.Hp < bossHealth.maxHp * 0.7 && guardStatue ==null)
        {
            //小於140
            bossHealth.animator.SetTrigger("Wheel_1_Broke");
            guardStatue = Instantiate(statue, transform.position + transform.forward * 3 , transform.rotation);
        }
        if (bossHealth.Hp < bossHealth.maxHp * 0.3 && Wheel2Trigger)
        {
            //小於60
            //停止但不消失 可能重複呼叫
            bossHealth.animator.SetTrigger("Wheel_2_Broke");
            Wheel2Trigger = false;
        }
    }
}
