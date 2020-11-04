using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public Transform playerRotation;
    public PlayerControl playerControl;
    SpriteRenderer spriteRenderer;
    public SkillControl skillControl;
    public bool isMagicAttack;
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        skillControl = FindObjectOfType<SkillControl>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //if (isMagicAttack)
        //{
        //    if (playerRotation.localEulerAngles.y < 180 && playerRotation.localEulerAngles.y > 0)
        //    {
        //        //Debug.Log("面向右邊");
        //        spriteRenderer.flipX = true;
        //    }
        //    else if (playerRotation.localEulerAngles.y < 360 && playerRotation.localEulerAngles.y > 180)
        //    {
        //        spriteRenderer.flipX = false;
        //    }
        //}
    }
    public void PlayerSpriteFlip()
    {
        if (playerRotation.localEulerAngles.y < 180 && playerRotation.localEulerAngles.y > 0)
        {
            //Debug.Log("面向右邊");
            spriteRenderer.flipX = true;
        }
        else if (playerRotation.localEulerAngles.y < 360 && playerRotation.localEulerAngles.y > 180)
        {
            spriteRenderer.flipX = false;
        }
    }
    void IsAttackFalse()//動畫Event控制
    {
        //為了攻擊中不能移動
        playerControl.isAttack = false;
    }
    void IsMagicAtttack()//動畫Event控制
    {
        //為了攻擊中不能移動
        //不能放在一開頭 不然會讀不到很詭異
        playerControl.isAttack = true;
        isMagicAttack = true;
    }
    void SkillShoot()//動畫Event控制
    {
        skillControl.skillList[0].Shoot();
        playerControl.isAttack = false;
        isMagicAttack = false;
    }
}
