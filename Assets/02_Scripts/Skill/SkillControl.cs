using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField]
    public Button[] buttonList;
    public Sprite[] spriteList;
    public Animator animator;
    public Skill[] skillList;

    public int CurIdx = 0;
    void Start()
    {
        SetPos();
    }
    void Update()
    {
        float move = Input.GetAxis("Mouse ScrollWheel");
        if (move > 0.0f)
        {
            if (CurIdx > 0) CurIdx--;
            SetPos();
        }
        else if (move < 0.0f)
        {
            if (CurIdx < buttonList.Length - 1) CurIdx++;
            SetPos();
        }
        if (Input.GetKeyDown(KeyCode.F5) && skillList[1].lastFireTime > skillList[1].fireRate)
        {
            skillList[0] = skillList[1];
            animator.SetTrigger("Magic");
            //會讓冷卻圖案怪怪的但是可以讓他不會抽搐
            skillList[1].lastFireTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.F6) && skillList[2].lastFireTime > skillList[2].fireRate)
        {
            skillList[0] = skillList[2];
            animator.SetTrigger("Magic");
            skillList[2].lastFireTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.F7) && skillList[3].lastFireTime > skillList[3].fireRate)
        {
            skillList[0] = skillList[3];
            animator.SetTrigger("Magic");
            skillList[3].lastFireTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.F8) && skillList[4].lastFireTime > skillList[4].fireRate)
        {
            skillList[0] = skillList[4];
            animator.SetTrigger("Magic");
            skillList[4].lastFireTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.F9) && skillList[5].lastFireTime > skillList[5].fireRate)
        {
            skillList[0] = skillList[5];
            animator.SetTrigger("Magic");
            skillList[5].lastFireTime = 0;
        }

        //滑鼠中鍵的效果
        if (Input.GetMouseButtonDown(2) && skillList[CurIdx + 1].lastFireTime > skillList[CurIdx + 1].fireRate)
        {
            skillList[CurIdx + 1].lastFireTime = 0;
            animator.SetTrigger("Magic");
            skillList[0] = skillList[CurIdx + 1];
        }
    }
    public void SetPos()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (i == CurIdx)
            {
                buttonList[i].enabled = true;
            }
            else
            {
                buttonList[i].enabled = false;
            }
        }
    }
}
