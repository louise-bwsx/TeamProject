using UnityEngine;

public class MobileSkillChoose : MonoBehaviour
{
    public Skill[] skillList;
    public MeshRenderer meshRenderer;
    public Animator animator;
    MobileAttack mobileAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        mobileAttack = FindObjectOfType<MobileAttack>();
    }
    public void SetSkill(int skillNum)
    {
        skillList[0] = skillList[skillNum];
        if (skillList[0].CanShoot() && !mobileAttack.isAttack)
        {
            //為了單獨讓指向性技能取消 不然下次按會直接射出
            animator.SetBool("IsCheck", false);
            meshRenderer.enabled = true;
            animator.SetTrigger("Magic");
            mobileAttack.isAttack = true;
        }
    }
    public void SkillShoot()//動畫Event控制
    {
        skillList[0].Shoot();
    }
}
