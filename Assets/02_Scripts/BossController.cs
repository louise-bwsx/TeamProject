using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyController
{
    public MeshRenderer meshRenderer;
    public Collider meleeAttackAreacollider;
    public Transform shootingtransform;
    public Door door;
    public GameObject arrow;
    public float force = 1500;
    float longRangeAttackCD;
    float meleeAttackCD;
    bool isMeleeAttack;
    void Start()
    {
        attackRate = 1.5f;
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
            //暫時想不到更好的方法關掉這些東東
            meshRenderer.enabled = false;
            isMeleeAttack = false;
            meleeAttackAreacollider.enabled = false;
        }
        //attackCD += Time.deltaTime;
        if (isMeleeAttack)
        {
            meleeAttackCD += Time.deltaTime;
        }
        //當門關起來的時候 且 與玩家的距離大於普攻距離
        if (door.isDoorClose = true && distence >= attackRaduis)
        {
            longRangeAttackCD += Time.deltaTime;
            if (longRangeAttackCD >= attackRate /*&&超出攻擊範圍*/)
            { 
                shootingtransform.LookAt(target);
                BossLongRangeAttack();
            }
        }
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
    void BossLongRangeAttack()
    {
        GameObject shootingArrow = Instantiate(arrow, shootingtransform.position, shootingtransform.rotation);
        shootingArrow.GetComponent<Rigidbody>().AddForce(shootingtransform.forward * force);
        Destroy(shootingArrow, 5f);
        longRangeAttackCD = 0;
    }
}
