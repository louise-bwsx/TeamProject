using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction: MonoBehaviour
{
    public Animator animator;
    public UIBarControl uIBarControl;
    public GameObject sword;
    public GameObject swingAttackEffectLeft;
    public GameObject swingAttackEffectRight;
    GameObject swordSpike;
    public Transform spwanPosition;
    public Transform playerRotation;
    public Transform player;
    SpriteRenderer spriteRenderer;
    PlayerControl playerControl;


    public AudioSource audioSource;//音效放置給所有怪物存取音效
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponentInParent<AudioSource>();
        playerControl = GetComponentInParent<PlayerControl>();
        audioSource.volume = CentralData.GetInst().SFXVol;
    }
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //GunAudio.PlayOneShot(walkSFX);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }
    public void Roll()
    {
        animator.SetTrigger("Roll");
        //音效
        audioSource.PlayOneShot(TurnOverSFX);
        //特效
    }
    public void NormalAttack()
    {
        //動畫
        animator.SetTrigger("Attack");
    }
    public void NormalAttackFX()//動畫Event呼叫
    {   //音效
        audioSource.PlayOneShot(SwingSFX);
        //特效
        GameObject FX;
        if (spriteRenderer.flipX == true)
        {
            FX = Instantiate(swingAttackEffectLeft, player);
            Destroy(FX, 0.3f);
        }
        else if (spriteRenderer.flipX == false)
        { 
            FX = Instantiate(swingAttackEffectRight, player);
            Destroy(FX, 0.3f);
        }
        
        //他只會抓這個方法啟動瞬間的範圍
        Collider[] hitEnemy = Physics.OverlapSphere(playerControl.AttackPoint.position, playerControl.attackRange, playerControl.EnemyLayer);
        foreach (Collider enemy in hitEnemy)
        {
            //這裡設定揮擊的特效 = 0
            enemy.GetComponent<MonsterHealth>()?.GetHit(playerControl.attackDamage);
        }
    }
    public void SpikeAttack()
    {
        audioSource.PlayOneShot(SpikeSFX);
        animator.SetTrigger("Attack_Spike");
        swordSpike = Instantiate(sword, spwanPosition.position, playerRotation.rotation);
        swordSpike.transform.parent = player.transform;
        Destroy(swordSpike, 0.3f);
    }
}
