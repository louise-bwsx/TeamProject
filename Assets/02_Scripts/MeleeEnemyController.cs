using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MeleeEnemyController : MonoBehaviour
{
    public float lookRaduis = 10;
    public float attackRaduis = 1;//近戰距離
    public GameObject attackCube;
    public Transform monsterAttackRotation;
    public Transform monsterOriginPos;
    public Animator animator;
    public float attackCD;
    public float attackRate = 1;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distence = Vector3.Distance(target.position, transform.position);
        float attackDistence = Vector3.Distance(target.position, transform.position);
        if (distence <= lookRaduis)
        {
            agent.enabled = true;
            //向著target走
            agent.SetDestination(target.position);
            //面對攝影機
            FaceCamera();
            //如果攻擊距離小於攻擊範圍 且 CD時間到
            if (attackDistence <= attackRaduis && attackCD > attackRate)
            {
                MonsterAttack();
            }
            //如果攻擊範圍大於攻擊距離
            else if (attackDistence > attackRaduis)
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
    void MonsterAttack()
    {
        //沒辦法取得animator.GetBool("IsAttack")的數值
        attackCube.SetActive(true);
        animator.SetTrigger("Attack");
        attackCD = 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRaduis);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRaduis);
    }
}
