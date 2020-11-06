using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EffectDirection
{ 
    Left,
    Right,
    Count
}

public class MobileAttack : MonoBehaviour
{
    public float attackCD;//同動畫時間
    public float normalAttackDash;
    public int spikeAttackDash;
    public bool isAttack;
    public GameObject swordCube;
    public Transform player;
    public Transform rollDirection;
    public Transform spawantransform;
    public Transform playerRotation;
    public AudioClip spikeSFX;//突刺音效
    public AudioClip swingSFX;//揮擊音效
    public GameObject[] attackEffect;
    public GameObject[] spikeEffect;
    public Transform[] spikeDirection;

    bool isSpike;
    float attackTimer;
    GameObject spwanSwordCube;
    Animator animator;
    AudioSource sourceSFX;
    SpriteRenderer spriteRenderer;
    Rigidbody RB;

    void Start()
    {
        RB = GetComponentInParent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sourceSFX = GetComponentInParent<AudioSource>();
        sourceSFX.volume = CentralData.GetInst().SFXVol;
    }
    void Update()
    {
        attackTimer += Time.deltaTime;
    }
    void Attack(string attackType)//按鈕呼叫
    {
        if (attackTimer >= attackCD)
        {
            //動畫
            animator.SetTrigger(attackType);
            //false在動畫Event呼叫
            isAttack = true;
            attackTimer = 0;
        }
    }
    void AttackRange(string attackType)//動畫Event呼叫
    {   
        //音效選擇
        switch (attackType)
        {
            case "Attack":
                {
                    sourceSFX.PlayOneShot(swingSFX);
                    break;
                }
            case "Attack_Spike":
                {
                    isSpike = true;
                    sourceSFX.PlayOneShot(spikeSFX);
                    spawantransform.GetComponent<Collider>().enabled = true;
                    break;
                }
        }
        //生成攻擊範圍
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        Destroy(spwanSwordCube, 0.3f);
    }
    void AttackEffect()//動畫Event呼叫
    {
        //特效
        GameObject FX;
        //根據人物面對位置調整特效方向
        switch (spriteRenderer.flipX)
        {
            case true:
                {
                    //左
                    if (isSpike)
                    {
                        FX = Instantiate(spikeEffect[(int)EffectDirection.Left], spikeDirection[(int)EffectDirection.Left].position, spikeDirection[(int)EffectDirection.Left].rotation);
                        Destroy(FX, 0.5f);
                        break;
                    }
                    FX = Instantiate(attackEffect[(int)EffectDirection.Left], player);
                    Destroy(FX, 0.3f);
                    break;
                }
            case false:
                {
                    //右
                    if (isSpike)
                    {
                        FX = Instantiate(spikeEffect[(int)EffectDirection.Right], spikeDirection[(int)EffectDirection.Right].position, spikeDirection[(int)EffectDirection.Right].rotation);
                        Destroy(FX, 0.5f);
                        break;
                    }
                    FX = Instantiate(attackEffect[(int)EffectDirection.Right], player);
                    Destroy(FX, 0.3f);
                    break;
                }
        }
    }
    void StartMoving()//動畫Event呼叫
    {
        if (isSpike)
        {
            RB.velocity = rollDirection.forward * spikeAttackDash;
            return;
        }
        //普攻向前移動
        RB.velocity = rollDirection.forward * normalAttackDash;
    }
    void StopMoving()//動畫Event呼叫
    {
        //踏步停止
        RB.velocity = Vector3.zero;
    }
    void DestroySword()//動畫Event呼叫
    {
        isSpike = false;
        //刪除攻擊範圍
        if (spwanSwordCube != null)
        { 
            Destroy(spwanSwordCube);
        }
        //關閉傷害範圍
        spawantransform.GetComponent<Collider>().enabled = false;
    }
    void IsAttackFalse()//動畫Event控制
    {
        //為了攻擊中不能移動
        isAttack = false;
    }
}
