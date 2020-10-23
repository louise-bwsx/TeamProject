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
  
    public GameObject bossUltArea;
    public int bossUltTimes;
    public bool isBossUlt;
    public Vector3 lookDirection; 
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
        lookDirection = Vector3.ProjectOnPlane(target.position - shootingtransform.position, shootingtransform.up);

        shootingtransform.rotation = Quaternion.LookRotation(lookDirection);
        Debug.DrawLine(shootingtransform.position, target.position, Color.green);
        //當玩家進入怪物偵測範圍 是否實時追蹤目標 是不是boss打輸的狀態
        if (distence <= detectRadius && attackCD > attackRate /*&& boss不是打輸的狀態*/)
        {
            //進入近戰攻擊範圍 && 避免CD時間一到又播一次動畫覆蓋攻擊動作
            if (distence <= meleeRadius && !meshRenderer.enabled)
            {
                meshRenderer.enabled = true;
                animator.SetTrigger("WeaponAttack");
                attackCD = 0;
            }
            //遠距離攻擊
            else if (distence >= meleeRadius)
            {
                meshRenderer.enabled = false;
                if (isBossUlt)
                {
                    BossUltAttack();
                    return;
                }
                if (bossHealth.Hp <= bossHealth.maxHp * 0.3)
                {
                    switch (Random.Range(0, 2))
                    {
                        case 0:
                            {
                                animator.SetTrigger("HandAttack");
                                attackCD = 0;
                                break;
                            }
                        case 1:
                            {
                                BossUltAttack();
                                break;
                            }
                    }
                }
                else if (bossHealth.Hp > bossHealth.maxHp * 0.3)
                {
                    animator.SetTrigger("HandAttack");
                    attackCD = 0;
                }
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
    }
    void BossLongRangeAttack()//由AnimatorEvent呼叫
    {
        GameObject shootingArrow = Instantiate(arrow, shootingtransform.position, shootingtransform.rotation);
        shootingArrow.GetComponent<Rigidbody>().AddForce(shootingtransform.forward * force);
        Destroy(shootingArrow, 3f);
    }
    public void BossUltAttack()
    {
        isBossUlt = true;
        Vector3 bossUltPosition = bossUltTransform.position;
        bossUltPosition.y = 15.7f;
        Instantiate(bossUltArea, bossUltPosition, shootingtransform.rotation);
        //每0.5秒鎖定玩家位置
        attackCD = 1.5f;
        bossUltTimes++;
        if (bossUltTimes >= 5)
        {
            bossUltTimes = 0;
            attackCD = 0;
            isBossUlt = false;
        }
    }
}
