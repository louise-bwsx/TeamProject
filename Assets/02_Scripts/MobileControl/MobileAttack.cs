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
    public bool isAttack;
    public GameObject swordCube;
    public Transform player;
    public Transform spawantransform;
    public Transform playerRotation;
    public AudioClip spikeSFX;//突刺音效
    public AudioClip swingSFX;//揮擊音效
    public float normalAttackDash;
    public GameObject[] attackEffect;

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
            //音效
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
                        break;
                    }
            }
            //sourceSFX.PlayOneShot(swingSFX);
            //false在動畫Event呼叫
            isAttack = true;
            attackTimer = 0;
        }
    }
    public void AttackRange()//動畫Event呼叫
    {
        //生成攻擊範圍
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        Destroy(spwanSwordCube, 0.3f);
    }
    public void AttackEffect()
    {
        //特效
        GameObject FX;
        //根據滑鼠位置調整特效方向
        switch (spriteRenderer.flipX)
        {
            case true:
                {
                    //左
                    FX = Instantiate(attackEffect[(int)EffectDirection.Left], player);
                    Destroy(FX, 0.3f);
                    break;
                }
            case false:
                {
                    //右
                    FX = Instantiate(attackEffect[(int)EffectDirection.Right], player);
                    Destroy(FX, 0.3f);
                    break;
                }
        }
    }
    public void RecoverMove()
    {
        isAttack = false;
    }
    public void StartMoving()
    {
        rigidbody.velocity = playerRotation.forward * normalAttackDash;
    }
    public void StopMoving()
    {
        rigidbody.velocity = Vector3.zero;
    }
    public void DestroySword()//動畫Event呼叫
    {
        //刪除攻擊範圍
        if (spwanSwordCube != null)
        { 
            Destroy(spwanSwordCube);
        }
        spawantransform.GetComponent<Collider>().enabled = false;
    }
}
