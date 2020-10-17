using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHealthControll : MonoBehaviour
{
    public GameObject guardStatue;
    BossHealth bossHealth;
    MonsterHealth monsterHealth;
    void Start()
    {
        bossHealth = FindObjectOfType<BossHealth>();
        monsterHealth = GetComponent<MonsterHealth>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            monsterHealth.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            monsterHealth.enabled = true;
        }
    }
    private void OnDestroy()
    {
        //當雕像被打掉時boss會受到10點傷害
        bossHealth.GetHit(10);
        //並且摧毀無敵區域
        Destroy(guardStatue);
        Debug.Log(1);
    }
}
