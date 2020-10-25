using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    public Transform brokenPos;
    public GameObject curseWheel;
    public GameObject brokenWheel;
    public GameObject bossDieDialogComponent;
    public GameObject ManiMenu;
    public float destroyTime = 2.5f;//環破碎所需要的時間
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
        if (Hp <= 0)
        {
            destroyTime -= Time.deltaTime;
            //聽說關不起來再關一次
            Destroy(invincibleGuard);
        }
        if (destroyTime <= -2)//多給兩秒的休息時間
        {
            bossDieDialogComponent.SetActive(true);
            if (BGMSource.clip != afterBGM)
            {
                BGMSource.Stop();
                BGMSource.clip = afterBGM;
                BGMSource.Play();
            }
        }
    }

    public override void MonsterDead()
    {
        audioSource.PlayOneShot(wheelBrokeSFX);
        GameObject FX = Instantiate(brokenWheel, brokenPos.position, brokenPos.rotation);
        Destroy(FX, destroyTime);
        base.MonsterDead();
    }
    public override void OnTriggerEnter(Collider other)
    {
        //Boss血量第一階段扣血條件
        if (Hp > maxHp * 0.7)
        {
            base.OnTriggerEnter(other);
            if (Hp <= maxHp * 0.7)
            {
                animator.SetTrigger("Wheel_1_Broke");
                audioSource.PlayOneShot(behindWheelBrokeSFX);
                invincibleGuard = Instantiate(BossInvincibleEffect, BossInvinciblePos.position, BossInvinciblePos.rotation);
                Time.timeScale = 0f;
                bossSecondStateDialog.SetActive(true);
            }
        }
        //Boss血量第二階段 當雕像打爆以後會繞過碰觸機制直接GetHit Boss
        else if (Hp <= maxHp * 0.7 && Hp>maxHp*0.3)
        {
            //測試用只要打開就不受階段限制
            base.OnTriggerEnter(other);
            //只有組合技才能造成傷害
            //if (other.CompareTag("Bomb"))
            //{
            //    //Destroy(BossInvincibleEffect);
            //    getHitEffect[0] = getHitEffect[2];
            //    audioSource.PlayOneShot(bombHitSFX);
            //    GetHit(30 + characterBase.INT);
            //    Debug.Log(1);
            //}
            //if (other.CompareTag("Firetornado") && hitByTransform != other.transform)
            //{
              
            //    getHitEffect[0] = getHitEffect[2];
            //    beAttackMin = beAttackMax;//最大被打的次數
            //    hitByTransform = other.transform;
            //    GetHit(5 + characterBase.INT);
            //    enumAttack = EnumAttack.fireTornado;
            //    Debug.Log(2);
            //}
            if (Hp <= maxHp * 0.3)
            {
                animator.SetTrigger("Wheel_2_Broke");
                audioSource.PlayOneShot(behindWheelBrokeSFX);
                bossController.BossUltAttack();
                Destroy(invincibleGuard);
                bossThirdStateDialog.SetActive(true);
            }
        }
        //Boss血量第三階段此時Boss開始會放大招
        else if (Hp <= maxHp * 0.3)
        {
            //聽說會有的時候沒關所以再加一個
            Destroy(invincibleGuard);
            base.OnTriggerEnter(other);
        }

        if (Hp <= 0 && curseWheel != null)
        {
            curseWheel.SetActive(false);
        }
    }
}