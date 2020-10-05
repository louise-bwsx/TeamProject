﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction: MonoBehaviour
{
    Animator animator;
    public UIBarControl uIBarControl;
    bool isAttack = true;
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
    }
    public void Roll()
    {
        //播動畫
        animator.SetTrigger("Roll");
        //音效
    }
    public void Attack()
    { 
        animator.SetTrigger("Attack");
    }
}
