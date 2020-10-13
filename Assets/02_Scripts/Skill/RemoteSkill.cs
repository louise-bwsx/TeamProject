﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSkill : Skill
{
    public LayerMask wall;
    public override void Shoot()
    {
        float cameraraylength = 100;
        Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit wallCross;
        if (lastFireTime > fireRate)
        {
            GameObject bulletObj = Instantiate(skillObject);
            if (Physics.Raycast(skillRotation.position, (skillPos.position - skillRotation.position), out wallCross, cameraraylength, wall))
            {
                Debug.Log(1);
                bulletObj.transform.position = wallCross.point;/*+ */
                bulletObj.transform.rotation = skillRotation.rotation;/*wallCross.collider.transform.rotation;*/
            }
            else
            {
                bulletObj.transform.position = skillPos.position;
                bulletObj.transform.rotation = skillRotation.rotation;
            }
        }
    }
}