using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    public GameObject attackCube;
    public Transform monsterAttackRotation;
    public Animator animator;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        distence = Vector3.Distance(target.position, transform.position);
        if (distence <= lookRaduis)
        {
            agent.enabled = true;
            //向著target走
            agent.SetDestination(target.position);
            //面對攝影機
            FaceCamera();
            //如果攻擊距離小於攻擊範圍 且 CD時間到
            if (distence <= attackRaduis && attackCD > attackRate)
            {
                MonsterAttack();
            }
            //如果攻擊範圍大於攻擊距離
            else if (distence > attackRaduis)
            {
                //怪物收刀避免碰撞
                attackCube.SetActive(animator.GetBool("IsAttack"));
            }
        }
        else if (distence >= lookRaduis)
        {
            agent.enabled = false;
        }
        attackCD += Time.deltaTime;
    }
    void FaceCamera()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        monsterAttackRotation.rotation = Quaternion.Slerp(monsterAttackRotation.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public void MonsterAttack()
    {
        attackCube.SetActive(true);
        animator.SetTrigger("Attack");
        attackCD = 0;
    }
}
