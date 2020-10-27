using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillControl : MonoBehaviour
{
    [SerializeField]
    public Image[] skillBorderList;
    public Sprite[] skillBorderChange;
    public Animator animator;
    public Skill[] skillList;
    public int CurIdx = 0;
    public PlayerControl playerControl;
    public GetHitEffect getHitEffect;

    void Start()
    {
        getHitEffect = FindObjectOfType<GetHitEffect>();
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
            if (CurIdx < skillBorderList.Length - 1) CurIdx++;
            SetPos();
        }
        //避免抽搐
        if (!playerControl.isAttack && getHitEffect.playerHealth>0)
        {
            if (Input.GetKeyDown(KeyCode.F5) && skillList[1].lastFireTime > skillList[1].fireRate)
            {
                animator.SetTrigger("Magic");
                skillList[0] = skillList[1];
            }
            if (Input.GetKeyDown(KeyCode.F6) && skillList[2].lastFireTime > skillList[2].fireRate)
            {
                animator.SetTrigger("Magic");
                skillList[0] = skillList[2];
            }
            if (Input.GetKeyDown(KeyCode.F7) && skillList[3].lastFireTime > skillList[3].fireRate)
            {
                animator.SetTrigger("Magic");
                skillList[0] = skillList[3];
            }
            if (Input.GetKeyDown(KeyCode.F8) && skillList[4].lastFireTime > skillList[4].fireRate)
            {
                animator.SetTrigger("Magic");
                skillList[0] = skillList[4];
            }
            if (Input.GetKeyDown(KeyCode.F9) && skillList[5].lastFireTime > skillList[5].fireRate)
            {
                animator.SetTrigger("Magic");
                skillList[0] = skillList[5];
            }
            //滑鼠中鍵的效果
            if (Input.GetMouseButtonDown(2) && skillList[CurIdx + 1].lastFireTime> skillList[CurIdx+1].fireRate)
            {
                //正式版一定要這樣才能讀的到
                playerControl.isAttack = true;
                animator.SetTrigger("Magic");
                skillList[0] = skillList[CurIdx + 1];
            }
        }

    }
    public void SetPos()
    {
        for (int i = 0; i < skillBorderList.Length; i++)
        {
            if (i == CurIdx)
            {
                //buttonList[i].enabled = true;
                skillBorderList[i].sprite = skillBorderChange[0];
            }
            else
            {
                //buttonList[i].enabled = false;
                skillBorderList[i].sprite = skillBorderChange[1];
            }
        }
    }
}
