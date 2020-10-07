using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField]
    public Button[] list;

    public SkillF5Launch F5Skill;
    public SkillF6Launch F6Skill;
    public SkillF7Launch F7Skill;
    public SkillF8Launch F8Skill;
    public SkillF9Launch F9Skill;

    public int CurIdx = 0;
    // Start is called before the first frame update
    void Start()
    {
        SetPos();
    }

    // Update is called once per frame
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
            if (CurIdx < list.Length - 1) CurIdx++;
            SetPos();
        }
        //滑鼠中鍵的效果
        if (Input.GetMouseButtonDown(2))
        {
            Button b = list[CurIdx];
            if (CurIdx == 0)
            {
                F5Skill.shoot();
            }
            if (CurIdx == 1)
            {
                F6Skill.shoot();
            }
            if (CurIdx == 2)
            {
                F7Skill.shoot();
            }

            if (CurIdx == 3)
            {
                F8Skill.shoot();
            }
            if (CurIdx == 4)
            {
                F9Skill.shoot();
            }
        }
    }
    public void SetPos()
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (i == CurIdx)
                list[i].enabled = true;
            else
                list[i].enabled = false;
        }


    }
}
