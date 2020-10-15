using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSkill : Skill
{
    public LayerMask wall;
    public override void Shoot()
    {
        float cameraraylength = 100;
        RaycastHit wallCross;
        GameObject bulletObj = Instantiate(skillObject);
        if (Physics.Raycast(skillRotation.position, (skillPos.position - skillRotation.position), out wallCross, cameraraylength, wall))
        {
            bulletObj.transform.position = wallCross.point;
            bulletObj.transform.rotation = skillRotation.rotation;
        }
        else
        {
            bulletObj.transform.position = skillPos.position;
            bulletObj.transform.rotation = skillRotation.rotation;
        }
        lastFireTime = 0;
        playerControl.stamina -= staminaCost;
    }
}
