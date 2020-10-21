using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUlt : MonoBehaviour
{
    public float timer;
    public float destroyTime = 1;
    public GameObject bossSkill;
    void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyTime)
        {
            GetComponent<Collider>().enabled = true;
            //可能會沒用
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        //物件刪除後新增特效
        GameObject bossUltEffect;
        bossUltEffect = Instantiate(bossSkill, transform.position, transform.rotation);
        Destroy(bossUltEffect, 0.5f);
    }
}
