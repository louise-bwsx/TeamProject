using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField]
    public Button[] buttonList;
    public Sprite[] spriteList;

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

        if (Input.GetKeyDown(KeyCode.F5))
        {
            skillList[0].Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            skillList[1].Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            skillList[2].Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            skillList[3].Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            skillList[4].Shoot();
        }

        //滑鼠中鍵的效果
        if (Input.GetMouseButtonDown(2))
        {
            if (CurIdx == 0)
            {
                skillList[0].Shoot();
            }
            if (CurIdx == 1)
            {
                skillList[1].Shoot();
            }
            if (CurIdx == 2)
            {
                skillList[2].Shoot();
            }
            if (CurIdx == 3)
            {
                skillList[3].Shoot();
            }
            if (CurIdx == 4)
            {
                skillList[4].Shoot();
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
}
