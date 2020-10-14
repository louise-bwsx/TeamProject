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
    public Transform spwan;
    public Transform player;


    public AudioSource audioSource;//音效放置給所有怪物存取音效
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效

    void Start()
    {
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
        audioSource.PlayOneShot(SpikeSFX);
        animator.SetTrigger("Attack_Spike");
        attackRate = 0;
        swordSpike = Instantiate(sword, spwan.position, spwan.rotation);
        Destroy(swordSpike, 0.3f);
    }
}
