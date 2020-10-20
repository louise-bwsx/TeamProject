using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHealthControll : MonsterHealth
{
    public GameObject guardStatue;//無敵雕像父物件
    public GameObject guradArea;//無敵區域
    public GameObject minionsGroup;//用來判斷小兵是否全部清完
    public BossFightRule bossFightRule;
    public Animator bossAnimator;
    public bool isInvincible = false;
    public override void Start()
    {
        base.Start();
        minionsGroup = GameObject.Find("MinionsGroup");
        bossFightRule = FindObjectOfType<BossFightRule>();
    }
    public override void Update()
    {
        base.Update();
        if (minionsGroup != null)
        { 
            if (minionsGroup.transform.childCount == 0 && bossFightRule.bossFightState == 1)
            {
                guradArea.GetComponent<MeshRenderer>().enabled = false;
                //關閉無敵狀態 單純關掉collider沒用
                guradArea.GetComponent<Collider>().isTrigger = false;
                minionsGroup.SetActive(false);
            }
        }
        if (bossFightRule.bossFightState == 3)
        {
            guradArea.GetComponent<MeshRenderer>().enabled = false;
            guradArea.GetComponent<Collider>().enabled = false;
            isInvincible = false;
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            Debug.Log("關閉無敵狀態");
            isInvincible = false;
        }
    }
    private void OnDestroy()
    {
        //當雕像被打掉時boss會受到10點傷害
        //bossHealth.GetHit(10);//Boss血量這個功能暫時被移除了
        switch (bossFightRule.bossFightState)
        {
            case 1:
                {
                    Debug.Log(1);
                    bossAnimator.SetTrigger("Wheel_1_Broke");
                    break;
                }
            case 2:
                {
                    bossAnimator.SetTrigger("Wheel_2_Broke");
                    break;
                }
        }

        bossFightRule.bossFightState++;
        //並且摧毀整個父物件
        Destroy(guardStatue);
        Destroy(minionsGroup);
        Debug.Log("雕像被摧毀");
    }
}
