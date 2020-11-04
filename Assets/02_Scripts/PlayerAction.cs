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
    public GameObject shadowDestory;
    public Transform player;
    public Transform spawantransform;
    public Transform SpikeAttackLeftPos;
    public Transform SpikeAttackRightPos;
    SpriteRenderer spriteRenderer;
    GameObject spwanSwordCube;
    public GameMenu gameMenu;
    public Transform playerRotation;

    public PlayerControl playerControl;
    public AudioSource BGMSource;
    public AudioSource SFXSource;//音效放置給所有怪物存取音效
    //public AudioClip walkSFX;//走路音效
    public AudioClip TurnOverSFX;//翻滾音效
    public AudioClip SpikeSFX;//突刺音效
    public AudioClip SwingSFX;//揮擊音效
    public AudioClip dieSFX;
    public PlayerFaceDirection playerFaceDirection;

    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        gameMenu = FindObjectOfType<GameMenu>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SFXSource = GetComponentInParent<AudioSource>();
        SFXSource.volume = CentralData.GetInst().SFXVol;
    }
    public void Roll()
    {
        animator.SetTrigger("Roll");
        //音效
        SFXSource.PlayOneShot(TurnOverSFX);
        //特效
    }

    //public void NormalAttack()
    //{
    //    //動畫
    //    animator.SetTrigger("Attack");
    //}
    //public void NormalAttackFX()//動畫Event呼叫
    //{   
    //    //生成攻擊範圍
    //    spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
    //}
    //public void NormalAttackEffect()
    //{
    //    //音效
    //    SFXSource.PlayOneShot(SwingSFX);
    //    //特效
    //    GameObject FX;
    //    if (playerRotation.localEulerAngles.y > 0 && playerRotation.localEulerAngles.y < 180)
    //    {
    //        FX = Instantiate(swingAttackEffectLeft, player);
    //        Destroy(FX, 0.3f);
    //    }
    //    else
    //    {
    //        FX = Instantiate(swingAttackEffectRight, player);
    //        Destroy(FX, 0.3f);
    //    }
    //}

    public void SpikeAttack()//動畫Event呼叫
    { 
        animator.SetTrigger("Attack_Spike");
        spawantransform.GetComponent<Collider>().enabled = true;
    }
    public void SpikeAttackFX()//動畫Event呼叫
    {
        //音效
        SFXSource.PlayOneShot(SpikeSFX);
        GameObject FX;
        spwanSwordCube = Instantiate(swordCube, spawantransform.position, spawantransform.rotation);
        //Destroy(spwanSwordCube, 0.5f);
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
    }
    //public void DestroySword()//動畫Event呼叫
    //{
    //    //刪除攻擊範圍
    //    Destroy(spwanSwordCube);
    //    spawantransform.GetComponent<Collider>().enabled = false;
    //}
    public void Die()//動畫Event呼叫
    {
        shadowDestory.SetActive(false);
        BGMSource.clip = dieSFX;
        SFXSource.PlayOneShot(dieSFX);
        gameMenu.anyWindow[6].SetActive(true);
        Time.timeScale = 0;
    }
    //public void StartMoving()
    //{
    //    playerControl.rigidbody.velocity = playerControl.playerRotation.forward * playerControl.normalAttackDash;
    //}
    //public void StopMoveing()
    //{
    //    playerControl.rigidbody.velocity = Vector3.zero;
    //}
    public void RollCancelAttack()
    {
        animator.SetBool("IsAttack", false);
        playerControl.isAttack = false;
        playerFaceDirection.isMagicAttack = false;
    }
}
