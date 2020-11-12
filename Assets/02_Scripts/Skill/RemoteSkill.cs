using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteSkill : Skill
{
    public LayerMask wall;
    public override void Shoot()
    {
        float cameraraylength = 100;
        RaycastHit wallCross;
        GameObject bulletObj = Instantiate(skillObject);
        //如果以 角色為中心 往 位置(技能位置-角色中心) 取出的值 小於距離 而且碰到的東西是wall
        if (Physics.Raycast(skillRotation.position, (skillPos.position - skillRotation.position), out wallCross, cameraraylength, wall))
        {
            bulletObj.transform.position = wallCross.point;
            bulletObj.transform.rotation = skillRotation.rotation;
        }
        else
        {
            Debug.Log("範圍型技能施放位置錯誤");
            bulletObj.transform.position = skillPos.position+skillPos.up*0.13f;
            bulletObj.transform.rotation = skillRotation.rotation;
        }
        skillTimer = 0;
        mobileStats.stamina -= staminaCost;
        mobileAttack.isAttack = false;
    }
}
