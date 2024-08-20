using System.Collections;
using UnityEngine;

public enum BossState
{
    /// <summary>
    /// Hp &gt; MaxHp * 0.7
    /// </summary>
    Stage0,
    /// <summary>
    /// Hp &gt; MaxHp * 0.4
    /// </summary>
    Stage1,
    /// <summary>
    /// 小於不能用 所以這樣替代 上面是大於沒錯
    /// Hp &lt;= MaxHp * 0.4
    /// </summary>
    Stage2
}

public class BossHealth : MonsterHealth
{
    [SerializeField] private Transform brokenPos;
    [SerializeField] private GameObject curseWheel;
    [SerializeField] private GameObject brokenWheel;
    [SerializeField] private GameObject BossInvincibleEffect;
    [SerializeField] private GameObject invincibleGuard;
    [SerializeField] private bool easyMode;

    private BossController bossController;
    //測試用只要打開就不受階段限制
    private BossState state;
    private const float WHEEL_DESTROY_TIME = 2.5f;//環破碎所需要的時間
    private float destroyTimer;
    private const float DESTROY_SOUND_TIME = 1.1f; //Boss血量歸零後幾秒要播放環破碎音效
    private float destroySoundTimer;

    protected override void Start()
    {
        base.Start();
        bossController = GetComponentInChildren<BossController>();
    }

    private IEnumerator BossDieCoroutine()
    {
        AudioManager.Inst.StopBGM();
        destroyTimer = WHEEL_DESTROY_TIME;
        destroySoundTimer = DESTROY_SOUND_TIME;
        while (true)
        {
            yield return null;
            if (!GameStateManager.Inst.IsGaming())
            {
                continue;
            }
            destroyTimer -= Time.deltaTime;
            destroySoundTimer -= Time.deltaTime;
            if (destroyTimer <= 0)
            {
                DialogueManager.Inst.ShowDialogue("BossDieDialogue");
                AudioManager.Inst.PlayBGM("AfterBossFight");
                break;
            }
        }
    }

    private IEnumerator DestroySoundCoroutine()
    {
        destroySoundTimer = DESTROY_SOUND_TIME;
        while (true)
        {
            yield return null;
            if (!GameStateManager.Inst.IsGaming())
            {
                continue;
            }
            destroySoundTimer -= Time.deltaTime;
            if (destroySoundTimer <= 0)
            {
                AudioManager.Inst.PlaySFX("WheelBrake");
                break;
            }
        }
    }

    protected override void GetHit(float Damage, SkillType type)
    {
        if (Hp <= 0)
        {
            return;
        }

        //TODOError: 第二階段不會扣寫
        if (state == BossState.Stage1)
        {
            if (type != SkillType.FireTornado && type != SkillType.Bomb)
            {
                return;
            }
            Debug.Log("二階段被打");
        }

        //不要再這邊設定invincibleTimer 為了讓風 可以打快一點
        if (invincibleTimer > 0)
        {
            //Debug.Log($"{transform.name} 無敵中");
            return;
        }
        Debug.Log($"Damage: {Damage}");

        SetHitEffect(type);

        //Boss不應該被玩家攻擊後重置攻擊CD

        //播放受擊動畫
        animator.SetTrigger("GetHit");

        //誰被打到
        // Debug.Log(transform.name);
        ObjectPool.Inst.SpawnFromPool(hitEffectName, (transform.position + Vector3.up) * 0.8f, transform.rotation, duration: 1f);
        Hp -= Damage;
        healthBarOnGame.SetHealth(Hp);
        StartCoroutine(InvincibleCoroutine());

        if (Hp > MaxHp * 0.7)
        {
            state = BossState.Stage0;
        }
        else if (Hp > MaxHp * 0.4 && state == BossState.Stage0)
        {
            ChangToState1();
        }
        else if (Hp <= MaxHp * 0.4 && state == BossState.Stage1)
        {
            ChangeToState2();
        }

        if (Hp <= 0)
        {
            MonsterDead();
            StartCoroutine(BossDieCoroutine());
            StartCoroutine(DestroySoundCoroutine());
        }
    }

    protected override void MonsterDead()
    {
        curseWheel.SetActive(false);
        healthBar.SetActive(false);
        GameObject FX = Instantiate(brokenWheel, brokenPos.position, brokenPos.rotation);
        Destroy(FX, WHEEL_DESTROY_TIME);
    }

    private void ChangToState1()
    {
        animator.SetTrigger("Wheel_1_Broke");
        AudioManager.Inst.PlaySFX("BehindWheelBrake");
        invincibleGuard.SetActive(true);
        Time.timeScale = 0f;
        DialogueManager.Inst.ShowDialogue("BossSecondStateDialogue");
        state = BossState.Stage1;
    }

    private void ChangeToState2()
    {
        animator.SetTrigger("Wheel_2_Broke");
        AudioManager.Inst.PlaySFX("BehindWheelBrake");
        bossController.BossUltAttack();
        invincibleGuard.SetActive(false);
        DialogueManager.Inst.ShowDialogue("BossThirdStateDialogue");
        state = BossState.Stage2;
    }
}