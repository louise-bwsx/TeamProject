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
    public GameObject SpikeAttackEffectLeft;
    public GameObject SpikeAttackEffectRight;
    public Transform player;
    public Transform spawantransform;
    public Transform SpikeAttackLeftPos;
    public Transform SpikeAttackRightPos;
    SpriteRenderer spriteRenderer;
    GameObject spwanSwordCube;
    public GameMenu gameMenu;


    public AudioSource audioSource;//音效放置給所有怪物存取音效
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效
    public AudioClip dieSFX;

    void Start()
    {
        gameMenu = FindObjectOfType<GameMenu>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.volume = CentralData.GetInst().SFXVol;
    }
    void Update()
    {
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        if (Input.GetAxis("Horizontal") != 0)
        {
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

    public void SpikeAttack()//動畫Event呼叫
    { 
        animator.SetTrigger("Attack_Spike");
        spawantransform.GetComponent<Collider>().enabled = true;
    }
    public void SpikeAttackFX()//動畫Event呼叫
    {
        //音效
        GameObject FX;
        audioSource.PlayOneShot(SpikeSFX);
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        if (spriteRenderer.flipX == false)
        {
            FX = Instantiate(SpikeAttackEffectLeft, SpikeAttackLeftPos.position, SpikeAttackLeftPos.rotation);
            Destroy(FX, 0.5f);
        }
        else if (spriteRenderer.flipX == true)
        {
            FX = Instantiate(SpikeAttackEffectRight, SpikeAttackRightPos.position, SpikeAttackRightPos.rotation);
            Destroy(FX, 0.5f);
        }
        //特效
    }
    public void DestroySword()//動畫Event呼叫
    {
        Destroy(spwanSwordCube);
        spawantransform.GetComponent<Collider>().enabled = false;
    }
    public void Die()//動畫Event呼叫
    {
        audioSource.clip = dieSFX;
        audioSource.PlayOneShot(dieSFX);
        gameMenu.anyWindow[6].SetActive(true);
        Time.timeScale = 0;
    }
}
