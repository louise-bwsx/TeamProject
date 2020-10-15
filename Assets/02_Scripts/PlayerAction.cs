using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction: MonoBehaviour
{
    public Animator animator;
    public UIBarControl uIBarControl;
    float attackRate;
    public GameObject sword;
    public GameObject swingAttackEffect;
    GameObject swordSpike;
    public Transform spwanPosition;
    public Transform playerRotation;
    public Transform player;
    SpriteRenderer spriteRenderer;


    public AudioSource audioSource;//音效放置給所有怪物存取音效
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponentInParent<AudioSource>();
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
        attackRate += Time.deltaTime;
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
        //音效
        audioSource.PlayOneShot(SwingSFX);
        //動畫
        animator.SetTrigger("Attack");
        //特效
        GameObject FX = Instantiate(swingAttackEffect, player);
        Destroy(FX, 1);
        //不讓他造成連按抖動
        attackRate = 0;
    }
    public void SpikeAttack()
    {
        if (playerRotation.localEulerAngles.y < 180 && playerRotation.localEulerAngles.y > 0)
        {
            spriteRenderer.flipX = true;
            //transform.rotation = Quaternion.Euler(-30, 90, 0);
        }
        else if (playerRotation.localEulerAngles.y < 360 && playerRotation.localEulerAngles.y > 180)
        {
            spriteRenderer.flipX = false;
            //transform.rotation = Quaternion.Euler(30, 270, 0);
        }
        audioSource.PlayOneShot(SpikeSFX);
        animator.SetTrigger("Attack_Spike");
        attackRate = 0;
        //位置不會移動
        //swordSpike = Instantiate(sword, spwan.position, spwan.rotation);
        //物體隨著parent太小,而且只能左右
        //swordSpike = Instantiate(sword, transform);
        swordSpike = Instantiate(sword, spwanPosition.position, playerRotation.rotation);
        swordSpike.transform.parent = player.transform;
        Destroy(swordSpike, 0.3f);
    }
}
