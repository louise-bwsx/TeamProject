using UnityEngine;

public class RemoteSkill : Skill
{
    private LayerMask wall;

    private void Start()
    {
        wall = LayerMask.GetMask("Wall");
    }

    public override void Shoot()
    {
        float cameraraylength = 100;
        RaycastHit hit;
        Rigidbody bulletObj = Instantiate(skillObject);
        if (Physics.Raycast(skillRotation.position, skillPos.position - skillRotation.position, out hit, cameraraylength, wall))
        {
            bulletObj.position = hit.point;
            bulletObj.rotation = skillRotation.rotation;
        }
        else
        {
            bulletObj.position = skillPos.position + skillPos.up * 0.13f;
            bulletObj.rotation = skillRotation.rotation;
        }
        skillCD = 0;
        //playerControl.stamina -= staminaCost;
    }
}