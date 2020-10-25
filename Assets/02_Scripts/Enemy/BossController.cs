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
    public Transform bossFxBossSlashPos;
    public BossHealth bossHealth;
    public GameObject arrow;
    public Transform swordPos;
    public GameObject BossSwordEffect;
    public float force = 1500;
  
    public GameObject bossUltArea;
    public GameObject bossFxBossSlash;
    public int bossUltTimes;
    public bool isBossUlt;
    public Vector3 lookDirection;
    public GameObject FX;
   AudioSource audioSource;
    public AudioClip bossSwordSFX;
    public AudioClip meleeAttackSFX;
    public AudioClip longRangeAttackSFX;
    public AudioClip bossSkillSFX;
    void Start()
    {
        bossHealth = GetComponentInParent<BossHealth>();
        animator = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.player.transform;
        if (audioSource == null)
        {
            audioSource = GetComponentInParent<AudioSource>();
        }
    }
    void Update()
    {
        //所有只要Boss死後應該做的事
        if (bossHealth.Hp <= 0)
        {
            //劍歸位
            animator.Play("Idle_Weapon");
            //關掉近戰碰撞
            meleeAttackAreacollider.enabled = false;
            //關掉近戰範圍
            meshRenderer.enabled = false;
            //歸零攻擊時間
            attackCD = 0;
            isBossUlt = false;
            //刪掉劍光
            Destroy(FX);
            return;
        }
        attackCD += Time.deltaTime;
        //Boss與玩家之間的距離
        float distence = Vector3.Distance(target.position, transform.position);
        //一個可以讓Z軸看向玩家方法 又不會改到X跟Z軸
        lookDirection = Vector3.ProjectOnPlane(target.position - shootingtransform.position, shootingtransform.up);
        shootingtransform.rotation = Quaternion.LookRotation(lookDirection);
        //所有遠程攻擊看向玩家的線
        Debug.DrawLine(shootingtransform.position, target.position, Color.green);
        if (distence >= meleeRadius)
        {
            meshRenderer.enabled = false;
        }
        //當玩家進入怪物偵測範圍 是否實時追蹤目標 是不是boss打輸的狀態
        if (distence <= detectRadius && attackCD > attackRate && bossHealth.Hp > 0)
        {
            //進入近戰攻擊範圍 && 避免CD時間一到又播一次動畫覆蓋攻擊動作
            if (distence <= meleeRadius && !meshRenderer.enabled)
            {
                if (meleeAttackAreacollider.enabled)
                {
                    meleeAttackAreacollider.enabled = false;
                }
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
    }
    void BossMeleeAttack()//由AnimatorEvent呼叫
    {
        //刪除劍光
        Destroy(FX);
        //近戰音效
        audioSource.PlayOneShot(meleeAttackSFX);
        //生成劍氣
        Instantiate(bossFxBossSlash, transform.position, transform.rotation);
        //預留好的攻擊Collider打開
        meleeAttackAreacollider.enabled = true;
        //攻擊範圍關閉
        meshRenderer.enabled = false;
    }
    void BossLongRangeAttack()//由AnimatorEvent呼叫
    {
        audioSource.PlayOneShot(longRangeAttackSFX);
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
        audioSource.PlayOneShot(bossSkillSFX);

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
    void BossSword()//由AnimatorEvent呼叫
    {
        //拔劍音效
        audioSource.PlayOneShot(bossSwordSFX);
        //生成劍光
        FX = Instantiate(BossSwordEffect, swordPos.position, swordPos.rotation);
        //近戰下一階段動畫條件確認
        animator.SetBool("IsWeaponAttack", true);
    }
}
