using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyController : EnemyController
{
    public GameObject attackCube;
    public Transform monsterAttackRotation;
    public MonsterHealth monsterHealth;
    public Material[] materials;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.Player.transform;
        agent = GetComponentInParent<NavMeshAgent>();
    }
    void Update()
    {
        attackCube.SetActive(true);
        distence = Vector3.Distance(target.position, transform.position);
        //如果小於偵測範圍
        if (distence <= detectRadius)
        {
            animator.SetBool("Walk", true);
            agent.enabled = true;
            //向著target走
            agent.SetDestination(target.position);
            //面對攝影機
            FaceCamera();
            //如果攻擊距離小於攻擊範圍 且 CD時間到
            if (distence <= meleeRadius && attackCD > attackRate)
            {
                attackCD = 0;
                animator.SetTrigger("Attack");
            }
            else if (distence >= meleeRadius && attackCD < attackRate)
            {
                //attackCube.GetComponent<MeshRenderer>().material = materials[0];
                attackCube.GetComponent<Collider>().enabled = false;
            }
        }
        else if (distence >= detectRadius)
        {
            animator.SetBool("Walk", false);
            attackCube.SetActive(false);
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
    void MonsterAttack()//動畫Event呼叫
    {
        //attackCube.GetComponent<MeshRenderer>().material = materials[1];
        attackCube.GetComponent<Collider>().enabled = true;
    }
}
