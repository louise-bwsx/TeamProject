using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyController
{
    public MeshRenderer meshRenderer;
    public Collider meleeAttackAreacollider;
    public Transform shootingtransform;
    public Transform bossUltTransform;
    public BossHealth bossHealth;
    public GameObject arrow;
    public float force = 1500;
    public GameObject[] doorClose;
    public GameObject bossUltArea;
    public List<Transform> bossUltTemp = new List<Transform>();
    public int bossUltTimes;
    bool isMeleeAttack;
    bool isLongRangeAttack;
    void Start()
    {
        bossHealth = GetComponentInParent<BossHealth>();
        animator = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.player.transform;
    }
    void Update()
    {
        attackCD += Time.deltaTime;
        float distence = Vector3.Distance(target.position, transform.position);
        shootingtransform.LookAt(target);
        //當玩家進入怪物偵測範圍 是否實時追蹤目標 是不是boss打輸的狀態
        if (distence <= detectRadius && attackCD > attackRate /*&& boss不是打輸的狀態*/)
        {
            foreach (GameObject i in doorClose)
            {
                i.SetActive(true);
            }
            //近戰攻擊
            if (distence <= meleeRadius)
            {
                meshRenderer.enabled = true;
                if (isMeleeAttack == false)
                {
                    animator.SetTrigger("WeaponAttack");
                    attackCD = 0;
                    isMeleeAttack = true;
                }
            }
            //遠距離攻擊
            else if (distence >= meleeRadius)
            {
                //當boss是第三階段
                if (isLongRangeAttack == false && bossUltTimes<5)
                //if (bossHealth.Hp < bossHealth.maxHp * 0.3 && bossUltTimes<5)
                {
                    GameObject bossUlt;
                    bossUlt = Instantiate(bossUltArea, bossUltTransform.position, shootingtransform.rotation);
                    Destroy(bossUlt, 1f);
                    attackCD = 1;
                    bossUltTimes++;
                    if (bossUltTimes >= 5)
                    {
                        attackCD = 0;
                        bossUltTimes = 0;
                    }
                }
                //if (isLongRangeAttack == false)
                //{
                //    animator.SetTrigger("HandAttack");
                //    attackCD = 0;
                //    isLongRangeAttack = true;
                //}
            }
        }
        if(attackCD<attackRate)
        {
            //暫時想不到更好的方法關掉這些東東
            meleeAttackAreacollider.enabled = false;
        }
    }
    void BossMeleeAttack()//由AnimatorEvent呼叫
    {
        meleeAttackAreacollider.enabled = true;
        meshRenderer.enabled = false;
        isMeleeAttack = false;
        attackCD = 0;
    }
    void BossLongRangeAttack()//由AnimatorEvent呼叫
    {
        GameObject shootingArrow = Instantiate(arrow, transform.position, shootingtransform.rotation);
        shootingArrow.GetComponent<Rigidbody>().AddForce(shootingtransform.forward * force);
        Destroy(shootingArrow, 5f);
        isLongRangeAttack = false;
    }
}
