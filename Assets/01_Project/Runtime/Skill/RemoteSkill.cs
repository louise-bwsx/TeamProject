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
        Vector3 spawanPos = skillPos.position + skillPos.up * 0.7f;
        Quaternion spawnRotation = skillRotation.rotation;
        GameObject skillObject = ObjectPool.Inst.SpawnFromPool(prefabName, spawanPos, spawnRotation, duration: destroyTime);
        if (skillObject == null)
        {
            return;
        }
        if (Physics.Raycast(skillRotation.position, skillPos.position - skillRotation.position, out hit, cameraraylength, wall))
        {
            skillObject.transform.position = hit.point;
        }
        else
        {
            skillObject.transform.position = skillPos.position + skillPos.up * 0.13f;
        }
        skillObject.transform.rotation = skillRotation.rotation;
        StartCoroutine(StartCoolDown());
    }
}