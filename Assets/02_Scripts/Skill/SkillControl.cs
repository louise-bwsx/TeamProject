using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField]
    public Button[] buttonList;
    public Sprite[] spriteList;
    PlayerControl playerControl;
    public Animator animator;
    public Skill[] skillList;

    public float F5ColdTime = 2;//技能CD時間 
    public float F5Timer = 0;//技能CD時間初始值
    public Image F5FilledImage;
    public bool F5IsStartTimer;//是否開始計算時間

    public float F6ColdTime = 2;//技能CD時間 
    public float F6Timer = 0;//技能CD時間初始值
    public Image F6FilledImage;
    public bool F6IsStartTimer;//是否開始計算時間

    public float F7ColdTime = 2;//技能CD時間 
    public float F7Timer = 0;//技能CD時間初始值
    public Image F7FilledImage;
    public bool F7IsStartTimer;//是否開始計算時間

    public float F8ColdTime = 2;//技能CD時間 
    public float F8Timer = 0;//技能CD時間初始值
    public Image F8FilledImage;
    public bool F8IsStartTimer;//是否開始計算時間

    public float F9ColdTime = 2;//技能CD時間 
    public float F9Timer = 0;//技能CD時間初始值
    public Image F9FilledImage;
    public bool F9IsStartTimer;//是否開始計算時間

    public int CurIdx = 0;
    void Start()
    {
        playerControl = FindObjectOfType<PlayerControl>();
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

        F5OnClick();
        F6OnClick();
        F7OnClick();
        F8OnClick();
        F9OnClick();

        //滑鼠中鍵的效果
        if (Input.GetMouseButtonDown(2))
        {
            animator.SetTrigger("Magic");
            if (CurIdx == 0)
            {
                skillList[0] = skillList[1];
                F5IsStartTimer = true;
                F5OnClick();
                //skillList[1].Shoot();
            }
            if (CurIdx == 1)
            {
                skillList[0] = skillList[2];
                F6OnClick();
                F6IsStartTimer = true;
                //skillList[2].Shoot();
            }
            if (CurIdx == 2)
            {
                skillList[0] = skillList[3];
                F7OnClick();
                F7IsStartTimer = true;
                //skillList[3].Shoot();
            }
            if (CurIdx == 3)
            {
                skillList[0] = skillList[4];
                F8OnClick();
                F8IsStartTimer = true;
                //skillList[4].Shoot();
            }
            if (CurIdx == 4)
            {
                skillList[0] = skillList[5];
                F9OnClick();
                F9IsStartTimer = true;
                //skillList[5].Shoot();
            }
        }
    }
    public void SetPos()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            if (i == CurIdx)
            {
                //buttonList[i].image.sprite = spriteList[i];
                buttonList[i].enabled = true;
            }
            else
            {
                //buttonList[i].image.sprite = spriteList[i];
                buttonList[i].enabled = false;
            }
        }
    }
    public void F5OnClick()
    {
        if (F5IsStartTimer)
        {
            F5Timer += Time.deltaTime;
            F5FilledImage.fillAmount = (F5ColdTime - F5Timer) / F5ColdTime;
        }
        if (F5Timer >= F5ColdTime)
        {
            F5FilledImage.fillAmount = 0;
            F5Timer = 0;
            F5IsStartTimer = false;
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            skillList[0] = skillList[1];
            animator.SetTrigger("Magic");
            F5IsStartTimer = true;
            //skillList[0].Shoot();
        }
    }

    public void F6OnClick()
    {
        if (F6IsStartTimer)
        {
            F6Timer += Time.deltaTime;
            F6FilledImage.fillAmount = (F6ColdTime - F6Timer) / F6ColdTime;
        }
        if (F6Timer >= F6ColdTime)
        {
            F6FilledImage.fillAmount = 0;
            F6Timer = 0;
            F6IsStartTimer = false;
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            skillList[0] = skillList[2];
            animator.SetTrigger("Magic");
            F6IsStartTimer = true;
            //skillList[1].Shoot();
        }
    }
    public void F7OnClick()
    {
        if (F7IsStartTimer)
        {
            F7Timer += Time.deltaTime;
            F7FilledImage.fillAmount = (F7ColdTime - F7Timer) / F7ColdTime;
        }
        if (F7Timer >= F7ColdTime)
        {
            F7FilledImage.fillAmount = 0;
            F7Timer = 0;
            F7IsStartTimer = false;
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            skillList[0] = skillList[3];
            animator.SetTrigger("Magic");
            F7IsStartTimer = true;
            //skillList[2].Shoot();
        }


    }
    public void F8OnClick()
    {
        if (F8IsStartTimer)
        {
            F8Timer += Time.deltaTime;
            F8FilledImage.fillAmount = (F8ColdTime - F8Timer) / F8ColdTime;
        }
        if (F8Timer >= F8ColdTime)
        {
            F8FilledImage.fillAmount = 0;
            F8Timer = 0;
            F8IsStartTimer = false;
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            skillList[0] = skillList[4];
            animator.SetTrigger("Magic");
            F8IsStartTimer = true;
            //skillList[3].Shoot();
        }

    }
    public void F9OnClick()
    {
        if (F9IsStartTimer)
        {
            F9Timer += Time.deltaTime;
            F9FilledImage.fillAmount = (F9ColdTime - F9Timer) / F9ColdTime;
        }
        if (F9Timer >= F9ColdTime)
        {
            F9FilledImage.fillAmount = 0;
            F9Timer = 0;
            F9IsStartTimer = false;
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            skillList[0] = skillList[5];
            animator.SetTrigger("Magic");
            F9IsStartTimer = true;
            //skillList[4].Shoot();
        }
    }
}
