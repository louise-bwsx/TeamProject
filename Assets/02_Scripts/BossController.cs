using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyController
{
    public MeshRenderer meshRenderer;
    public Collider meleeAttackAreacollider;
    public Transform shootingtransform;
    public GameObject arrow;
    public float force = 1500;
    bool isMeleeAttack;
    bool isLongRangeAttack;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.player.transform;
    }
    void Update()
    {
        attackCD += Time.deltaTime;
        float distence = Vector3.Distance(target.position, transform.position);
        if (distence <= detectRadius && attackCD > attackRate)
        { 

            ////近戰攻擊
            //if (distence <= meleeRadius)
            //{
            //    meshRenderer.enabled = true;
            //    if (isMeleeAttack == false)
            //    {
            //        animator.SetTrigger("WeaponAttack");
            //        attackCD = 0;
            //        isMeleeAttack = true;
            //    }
            //}
            ////遠距離攻擊
            //else if (distence >= meleeRadius)
            //{
            //    shootingtransform.LookAt(target);
            //    if (isLongRangeAttack == false)
            //    {
            //        animator.SetTrigger("HandAttack");
            //        attackCD = 0;
            //        isLongRangeAttack = true;
            //    }
            //}
        }
        if(attackCD<attackRate)
        {
            //暫時想不到更好的方法關掉這些東東
            meleeAttackAreacollider.enabled = false;
        }
    }
    void BossMeleeAttack()//由AnimatorEvent呼叫
    {
        meleeAttackAreacollider.enabled = true;
        meshRenderer.enabled = false;
        isMeleeAttack = false;
        attackCD = 0;
    }
    void BossLongRangeAttack()//由AnimatorEvent呼叫
    {
        GameObject shootingArrow = Instantiate(arrow, shootingtransform.position, shootingtransform.rotation);
        shootingArrow.GetComponent<Rigidbody>().AddForce(shootingtransform.forward * force);
        Destroy(shootingArrow, 5f);
        isLongRangeAttack = false;
    }
}
