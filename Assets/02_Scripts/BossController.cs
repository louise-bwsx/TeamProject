using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyController
{
    public MeshRenderer meshRenderer;
    public Collider meleeAttackAreacollider;
    public float meleeAttackCD;
    bool isMeleeAttack;
    void Start()
    {
        attackRate = 2;
        target = PlayerManager.instance.player.transform;
    }
    void Update()
    {
        float distence = Vector3.Distance(target.position, transform.position);
        if (distence <= attackRaduis)
        {
            isMeleeAttack = true;
            BossMeleeAttack();
        }
        else if (distence >= attackRaduis)
        {
            meshRenderer.enabled = false;
            isMeleeAttack = false;
            //暫時想不到更好的方法關掉它
            meleeAttackAreacollider.enabled = false;
        }
        //attackCD += Time.deltaTime;
        if (isMeleeAttack)
        {
            meleeAttackCD += Time.deltaTime;
        }
        Debug.Log(meleeAttackCD);
        Debug.Log(isMeleeAttack);
    }
    void BossMeleeAttack()
    {
        meshRenderer.enabled = true;
        if (meleeAttackCD > attackRate)
        {
            meleeAttackAreacollider.enabled = true;
            meshRenderer.enabled = false;
            meleeAttackCD = 0;
        }
    }
}
