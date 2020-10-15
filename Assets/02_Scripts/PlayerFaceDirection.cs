using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceDirection : MonoBehaviour
{
    public PlayerControl playerControl;
    public Transform playerRotation;
    public bool isAttack = false;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (playerControl.cantMove == false && Time.timeScale != 0 && isAttack == false)
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
            else if (isAttack == false)
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
        }
    }
    void isAttackTrue()//動畫Event控制
    {
        isAttack = true;
    }
    void isAttackFalse()//動畫Event控制
    {
        isAttack = false;
    }
}
