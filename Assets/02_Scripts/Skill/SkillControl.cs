using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField]
    public Button[] list;


    public Skill[] skillList;
    //public SkillF5Launch skillF5Launch;
    //public SkillF6Launch f6Skill;
    //public SkillF7Launch f7Skill;
    //public SkillF8Launch f8Skill;
    //public SkillF9Launch f9Skill;

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

        if (Input.GetKeyDown(KeyCode.F5))
        {
            skillList[0].Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            //F6Skill.Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            //F7Skill.Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            //F8Skill.Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            //F9Skill.Shoot();
        }

        //滑鼠中鍵的效果
        if (Input.GetMouseButtonDown(2))
        {
            while(CurIdx<list.Length)
            if (CurIdx == 0)
            {
                skillF5Launch.Shoot();
            }
            if (CurIdx == 1)
            {
                //f6Skill.shoot();
            }
            if (CurIdx == 2)
            {
                //f7Skill.shoot();
            }

            if (CurIdx == 3)
            {
                //f8Skill.shoot();
            }
            if (CurIdx == 4)
            {
                //f9Skill.shoot();
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
