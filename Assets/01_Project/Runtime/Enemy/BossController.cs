using UnityEngine;

public class BossController : EnemyController
{
    public MeshRenderer meshRenderer;
    public Transform shootingtransform;
    public Transform bossUltTransform;
    public Transform bossFxBossSlashPos;
    public GameObject bossUltAim;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private Rigidbody fireBall;
    public Transform swordPos;
    public GameObject BossSwordEffect;
    public float force = 1500;

    [SerializeField] private GameObject bossUltArea;
    public GameObject bossFxBossSlash;
    public int bossUltTimes;
    public bool isBossUlt;
    public Vector3 lookDirection;
    public GameObject FX;

    private void Awake()
    {
        bossHealth = GetComponentInParent<BossHealth>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //玩家生成沒有Awake快
        target = PlayerManager.Inst.Player.transform;
    }

    void Update()
    {
        //所有只要Boss死後應該做的事
        if (bossHealth.Hp <= 0)
        {
            //劍歸位
            animator.Play("Idle_Weapon");
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
        //當玩家進入怪物偵測範圍 是否實時追蹤目標 是不是boss打輸的狀態
        if (distence <= detectRadius && attackCD > attackRate && bossHealth.Hp > 0)
        {
            //進入近戰攻擊範圍 && 避免CD時間一到又播一次動畫覆蓋攻擊動作
            if (distence <= meleeRadius && !meshRenderer.enabled)
            {
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
                if (bossHealth.Hp <= bossHealth.MaxHp * 0.3)
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
                else if (bossHealth.Hp > bossHealth.MaxHp * 0.3)
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
        AudioManager.Inst.PlaySFX("MeleeAttack");
        //生成劍氣
        GameObject swordAttackFX = Instantiate(bossFxBossSlash, transform.position, transform.rotation);
        Destroy(swordAttackFX, 0.5f);
        //攻擊範圍關閉
        meshRenderer.enabled = false;
    }

    void BossLongRangeAttack()//由AnimatorEvent呼叫
    {
        AudioManager.Inst.PlaySFX("RangeAttack");
        Rigidbody projectile = Instantiate(fireBall, shootingtransform.position, shootingtransform.rotation);
        projectile.AddForce(shootingtransform.forward * force);
        Destroy(projectile, 2f);
    }

    public void BossUltAttack()
    {
        GameObject FX = Instantiate(bossUltAim, transform.position, transform.rotation);
        Destroy(FX, 1f);
        isBossUlt = true;
        Vector3 bossUltPosition = bossUltTransform.position;
        bossUltPosition.y = 15.7f;
        Instantiate(bossUltArea, bossUltPosition, shootingtransform.rotation);
        AudioManager.Inst.PlaySFX("BossUlt");

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
        meshRenderer.enabled = true;
        //拔劍音效
        AudioManager.Inst.PlaySFX("SwordDraw");
        //生成劍光
        FX = Instantiate(BossSwordEffect, swordPos.position, swordPos.rotation);
        //近戰下一階段動畫條件確認
        animator.SetBool("IsWeaponAttack", true);
    }
}