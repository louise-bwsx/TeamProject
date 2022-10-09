using UnityEngine;

public class BossHealth : MonsterHealth
{
    public Transform brokenPos;
    public GameObject curseWheel;
    public GameObject brokenWheel;
    public GameObject bossDieDialogComponent;
    public GameObject ManiMenu;
    public float destroyTime = 2.5f;//環破碎所需要的時間
    public float destroySoundTime = 1f; //Boss血量歸零後幾秒要播放環破碎音效
    public float timer;
    BossController bossController;
    public GameObject BossInvincibleEffect;
    public Transform BossInvinciblePos;
    public GameObject bossSecondStateDialog;
    public GameObject bossThirdStateDialog;
    public GameObject invincibleGuard;
    public AudioClip behindWheelBrokeSFX;
    public AudioClip wheelBrokeSFX;
    public AudioClip afterBGM;
    public AudioSource BGMSource;
    public bool easyMode;
    public bool[] bossState = new bool[2];


    public override void Start()
    {
        base.Start();
        bossController = FindObjectOfType<BossController>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    public override void Update()
    {
        base.Update();
        if (Hp <= 0 && destroyTime>-2)
        {
            BGMSource.Stop();
            destroyTime -= Time.deltaTime;
        }
        if (destroyTime <= -2)//多給兩秒的休息時間
        {
            bossDieDialogComponent.SetActive(true);
            AudioManager.Inst.PlayBGM("AfterBossFight");
        }
        if (Hp <= maxHp * 0.7 && !bossState[0])
        {
            animator.SetTrigger("Wheel_1_Broke");
            audioSource.PlayOneShot(behindWheelBrokeSFX);
            invincibleGuard = Instantiate(BossInvincibleEffect, BossInvinciblePos.position, BossInvinciblePos.rotation);
            Time.timeScale = 0f;
            bossSecondStateDialog.SetActive(true);
            bossState[0] = true;
        }
        else if (Hp <= maxHp * 0.3 && !bossState[1])
        {
            animator.SetTrigger("Wheel_2_Broke");
            audioSource.PlayOneShot(behindWheelBrokeSFX);
            bossController.BossUltAttack();
            Destroy(invincibleGuard);
            //第三階段提示
            bossThirdStateDialog.SetActive(true);
            bossState[1] = true;
        }
        if (Hp <= 0 && timer < destroySoundTime)
        {
            timer += Time.deltaTime;
            if (timer >= destroySoundTime)
            {
                audioSource.PlayOneShot(wheelBrokeSFX);
            }
        }
    }

    public override void MonsterDead()
    {
        curseWheel.SetActive(false);
        GameObject FX = Instantiate(brokenWheel, brokenPos.position, brokenPos.rotation);
        Destroy(FX, destroyTime);
        //base.MonsterDead();
    }
    public override void OnTriggerEnter(Collider other)
    {
        //Boss血量第一階段扣血條件
        if (Hp > maxHp * 0.7)
        {
            base.OnTriggerEnter(other);
        }
        //Boss血量第二階段
        if (Hp <= maxHp * 0.7 && Hp > maxHp * 0.3)
        {
            if (easyMode)
            {
                //測試用只要打開就不受階段限制
                base.OnTriggerStay(other);
            }
            //只有組合技才能造成傷害
            if (other.CompareTag("Bomb"))
            {
                //Destroy(BossInvincibleEffect);
                getHitEffect[0] = getHitEffect[2];
                audioSource.PlayOneShot(bombHitSFX);
                hitByTransform = other.transform;
                GetHit(60 + characterBase.charaterStats[(int)CharacterStats.INT] + characterBase.charaterStats[(int)CharacterStats.SPR] * 2);
            }
        }
        //Boss血量第三階段此時Boss開始會放大招
        else if (Hp <= maxHp * 0.3)
        {
            base.OnTriggerEnter(other);
        }
    }
    public override void OnTriggerStay(Collider other)
    {    //Boss血量第一階段扣血條件
        if (Hp > maxHp * 0.7)
        {
            base.OnTriggerStay(other);
        }
        //Boss血量第二階段
        else if (Hp <= maxHp * 0.7 && Hp > maxHp * 0.3)
        {
            if (easyMode)
            {
                //測試用只要打開就不受階段限制
                base.OnTriggerStay(other);
            }
            if (other.CompareTag("Firetornado"))
            {

                getHitEffect[0] = getHitEffect[2];
                if (beAttackTime > attackTime)
                {
                    GetHit(5 + characterBase.charaterStats[(int)CharacterStats.INT] + characterBase.charaterStats[(int)CharacterStats.SPR] * 2 + skillBase.windSkillLevel * 20);
                    //怪物被受擊的間隔時間歸零
                    beAttackTime = 0;
                }
                enumAttack = EnumAttack.fireTornado;
            }
        }
        //Boss血量第三階段此時Boss開始會放大招
        else if (Hp <= maxHp * 0.3)
        {
            base.OnTriggerStay(other);
        }
    }
}