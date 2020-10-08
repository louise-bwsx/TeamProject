using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction: MonoBehaviour
{
    public Animator animator;
    public UIBarControl uIBarControl;
    bool isAttack = true;
    float attackRate;
    public float backToIdle = 0.5f;
    public GameObject sword;
    public Transform spwan;

    public AudioSource GunAudio;//音樂放置
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (attackRate > backToIdle)
        {
            animator.SetBool("IsAttack", !isAttack);
        }
        attackRate += Time.deltaTime;
    }
    public void Roll()
    {
        animator.SetTrigger("Roll");
        //音效
        GunAudio.PlayOneShot(TurnOverSFX);
        //特效
    }
    public void NormalAttack()
    {
        //音效
        GunAudio.PlayOneShot(SwingSFX);
        //特效
        animator.SetTrigger("Attack");
        //不讓他造成連按抖動
        attackRate = 0;
    }
    public void SpikeAttack()
    {
        GunAudio.PlayOneShot(SpikeSFX);
        animator.SetTrigger("Attack_Spike");
        GameObject attackingSword = Instantiate(sword, spwan.position, spwan.rotation);
        attackRate = 0;
        Destroy(attackingSword,0.3f);
    }
}
