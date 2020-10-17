using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHealthControll : MonsterHealth
{
    public GameObject guardStatue;//無敵雕像父物件
    public GameObject guradArea;//無敵區域
    public GameObject minionsGroup;//用來判斷小兵是否全部清完
    public BossFightRule bossFightRule;
    public bool isInvincible = false;

    //BossHealth bossHealth;
    //MonsterHealth monsterHealth;
    public override void Start()
    {
        base.Start();
        minionsGroup = GameObject.Find("MinionsGroup");
        bossFightRule = FindObjectOfType<BossFightRule>();
        //bossHealth = FindObjectOfType<BossHealth>();
        //monsterHealth = GetComponent<MonsterHealth>();
    }
    public override void Update()
    {
        base.Update();
        Debug.Log(minionsGroup.transform.childCount);
        if (minionsGroup.transform.childCount == 0 /*&& minionsGroup !=null*/ && bossFightRule.bossFightState == 1)
        {
            guradArea.GetComponent<MeshRenderer>().enabled = false;
            guradArea.GetComponent<Collider>().isTrigger = false;//關閉無敵狀態 單純關掉collider沒用
            //關太快會讀不到OnTriggerExit的瞬間反而無法關閉無敵狀態
            //guradArea.GetComponent<Collider>().enabled = false;//關閉collider避免形成空氣牆
            //刪除以後minionsGroup欄位會顯示遺失但不等於null所以會一直跳紅字
            //Destroy(minionsGroup);
            minionsGroup.SetActive(false);
        }
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (isInvincible == false)
        {
            base.OnTriggerEnter(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            isInvincible = true;
            //monsterHealth.enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            Debug.Log("關閉無敵狀態");
            isInvincible = false;
            //monsterHealth.enabled = true;
        }
    }
    private void OnDestroy()
    {
        //當雕像被打掉時boss會受到10點傷害
        //bossHealth.GetHit(10);//Boss血量這個功能暫時被移除了

        //並且摧毀整個父物件
        Destroy(guardStatue);
        Destroy(minionsGroup);
        Debug.Log("雕像被摧毀");
    }
}
