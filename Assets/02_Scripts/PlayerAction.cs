using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Animator animator;
    //public UIBarControl uIBarControl;
    public GameObject swordCube;
    public GameObject swingAttackEffectLeft;
    public GameObject swingAttackEffectRight;
    public Transform player;
    public Transform spawantransform;
    SpriteRenderer spriteRenderer;
    GameObject spwanSwordCube;


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
    {   
        //音效
        audioSource.PlayOneShot(SwingSFX);
        //特效
        GameObject FX;
        //左右特效不能flip
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
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
    }

    public void SpikeAttack()
    { 
        animator.SetTrigger("Attack_Spike");
    }
    public void SpikeAttackFX()
    {
        //音效
        audioSource.PlayOneShot(SpikeSFX);
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        //特效
    }
    public void DestroySword()
    {
        Destroy(spwanSwordCube);
    }
}
