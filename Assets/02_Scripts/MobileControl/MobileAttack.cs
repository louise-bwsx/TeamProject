using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    public bool isAttack;
    public GameObject swordCube;
    public Transform player;
    public Transform spawantransform;
    public Transform playerRotation;
    public AudioClip spikeSFX;//突刺音效
    public AudioClip swingSFX;//揮擊音效
    public float normalAttackDash;
    public GameObject[] attackEffect;
    public GameObject[] spikeEffect;
    public Transform spikeDirection;

    float attackTimer;
    GameObject spwanSwordCube;
    Animator animator;
    AudioSource sourceSFX;
    SpriteRenderer spriteRenderer;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sourceSFX = GetComponentInParent<AudioSource>();
        sourceSFX.volume = CentralData.GetInst().SFXVol;
    }
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        attackTimer += Time.deltaTime;
    }
    public void Attack(string attackType)
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
                    sourceSFX.PlayOneShot(spikeSFX);
                    spawantransform.GetComponent<Collider>().enabled = true;
                    break;
                }
        }
        //生成攻擊範圍
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        Destroy(spwanSwordCube, 0.3f);
    }
    void AttackEffect(bool isSpike = false)//動畫Event呼叫
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
                        FX = Instantiate(spikeEffect[(int)EffectDirection.Left], spikeDirection.position, spikeDirection.rotation);
                        Destroy(FX, 0.5f);
                        return;
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
                        FX = Instantiate(spikeEffect[(int)EffectDirection.Right], spikeDirection.position, spikeDirection.rotation);
                        Destroy(FX, 0.5f);
                        return;
                    }
                    FX = Instantiate(attackEffect[(int)EffectDirection.Right], player);
                    Destroy(FX, 0.3f);
                    break;
                }
        }
    }
    void SpikeEffect()//動畫Event呼叫
    {
        //GameObject FX;
        //Switch(spriteRenderer.flipX)
        //if (spriteRenderer.flipX == false)
        //{
        //    FX = Instantiate(SpikeAttackEffectLeft, SpikeAttackLeftPos.position, SpikeAttackLeftPos.rotation);
        //    Destroy(FX, 0.5f);
        //}
        //else if (spriteRenderer.flipX == true)
        //{

        //    FX = Instantiate(SpikeAttackEffectRight, SpikeAttackRightPos.position, SpikeAttackRightPos.rotation);
        //    Destroy(FX, 0.5f);
        //}
    }
    void StartMoving()//動畫Event呼叫
    {
        //普攻向前移動
        rigidbody.velocity = playerRotation.forward * normalAttackDash;
    }
    void StopMoving()//動畫Event呼叫
    {
        //踏步停止
        rigidbody.velocity = Vector3.zero;
    }
    void DestroySword()//動畫Event呼叫
    {
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
