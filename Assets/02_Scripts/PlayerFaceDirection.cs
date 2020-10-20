using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public Transform playerRotation;
    public PlayerControl playerControl;
    SpriteRenderer spriteRenderer;
    public SkillControl skillControl;
    void Start()
    {
        playerControl = GetComponentInParent<PlayerControl>();
        skillControl = FindObjectOfType<SkillControl>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (playerControl.isAttack)
        {
            if (playerRotation.localEulerAngles.y < 180 && playerRotation.localEulerAngles.y > 0)
            {
                //Debug.Log("面向右邊");
                spriteRenderer.flipX = true;
                //transform.rotation = Quaternion.Euler(-30, 90, 0);
            }
            else if (playerRotation.localEulerAngles.y < 360 && playerRotation.localEulerAngles.y > 180)
            {
                spriteRenderer.flipX = false;
                //transform.rotation = Quaternion.Euler(30, 270, 0);
            }
        }
        else if (playerControl.cantMove == false && Time.timeScale != 0 && playerControl.isAttack == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                spriteRenderer.flipX = true;
                //transform.rotation = Quaternion.Euler(-30, 90, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                spriteRenderer.flipX = false;
                //transform.rotation = Quaternion.Euler(30, 270, 0);
            }
        }
    }
    void IsAttackFalse()//動畫Event控制
    {
        playerControl.isAttack = false;
    }
    void IsMagicAtttack()//動畫Event控制
    {
        //不能放在一開頭 不然會讀不到很詭異
        playerControl.isAttack = true;
    }
    void SkillShoot()//動畫Event控制
    {
        skillControl.skillList[0].Shoot();
        playerControl.isAttack = false;
    }
}
