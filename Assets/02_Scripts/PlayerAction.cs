using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction: MonoBehaviour
{
    public Animator animator;
    public UIBarControl uIBarControl;
    float attackRate;
    bool isSpikeAttack = false;
    public GameObject sword;
    public GameObject swingAttackEffect;
    GameObject swordSpike;
    public Transform spwan;
    public Transform player;


    public AudioSource audioSource;//音樂放置
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效
    void Update()
    {
        Debug.Log(animator.GetBool("IsAttack"));
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

        if (animator.GetBool("IsAttack") && swordSpike == null && isSpikeAttack)
        {
            Debug.Log(1);
            swordSpike = Instantiate(sword, spwan.position, spwan.rotation);
        }
        if (!animator.GetBool("IsAttack"))
        {
            Destroy(swordSpike);
            isSpikeAttack = false;
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
        isSpikeAttack = true;
    }
}
