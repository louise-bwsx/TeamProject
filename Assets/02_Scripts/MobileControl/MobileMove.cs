using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileMove : MonoBehaviour
{
    public FixedJoystick leftJoyStick;
    public float ws;
    public float ad;
    public float moveSpeed;
    public bool isAttack = false;
    SpriteRenderer spriteRenderer;
    Rigidbody rigidbody;
    CharacterBase characterBase;
    Animator animator;
    Vector3 moveMent;
    void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        characterBase = FindObjectOfType<CharacterBase>();
    }
    void FixedUpdate()
    {
        ws = leftJoyStick.Vertical;
        ad = leftJoyStick.Horizontal;
        //攻擊時不能移動
        if (!isAttack && !animator.GetBool("IsAttack"))
        {
            moveMent.Set(-ws, 0f, ad);
        }
        else
        {
            moveMent.Set(0, 0, 0);
        }
        //如果有Movement.normalized會延遲很嚴重 因為四捨五入?
        moveMent = moveMent * (moveSpeed + characterBase.charaterStats[(int)CharacterStats.AGI]) * Time.deltaTime;
        rigidbody.MovePosition(transform.position + moveMent);
    }
    void Update()
    {
        animator.SetFloat("Vertical", ws);
        if (ad != 0 && ad>Mathf.Abs(ws))
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        if (Time.timeScale != 0 && isAttack == false && !animator.GetBool("IsAttack"))
        {
            if (ad>0)
            {
                spriteRenderer.flipX = true;
            }
            else if (ad<0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
