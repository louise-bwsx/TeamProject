using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSkill : MonoBehaviour
{
    Animator animator;
    public Skill[] skillList;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetSkill(int skillNum)
    {
        animator.SetTrigger("Magic");
        skillList[0] = skillList[skillNum];
    }
}
