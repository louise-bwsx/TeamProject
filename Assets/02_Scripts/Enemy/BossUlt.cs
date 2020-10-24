using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUlt : MonoBehaviour
{
    public float timer;
    public GameObject bossSkill;
    void Update()
    {
        timer += Time.deltaTime;
        //0.5秒後將區域變成傷害判定區域
        if (timer > 0.5f && GetComponent<Collider>().enabled == false)
        {
            GetComponent<Collider>().enabled = true;
            GameObject bossUltEffect;
            Vector3 bossSkillPosition = transform.position;
            bossSkillPosition.y = 16f;
            bossUltEffect = Instantiate(bossSkill, bossSkillPosition+ transform.up*0.3f, transform.rotation);

            Destroy(gameObject, 1.5f);
            Destroy(bossUltEffect, 1.5f);
        }
    }
}
