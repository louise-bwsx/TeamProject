using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float lookRaduis = 10;
    //public float attackRaduis = 5;//遠攻距離
    public float attackRaduis = 1;
    public GameObject attackCylinder;
    public Transform MonsterTexure;
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
            agent.SetDestination(target.position);
            if (distence <= agent.stoppingDistance)
            {
                //攻擊目標
                //面對目標 這裡要改z軸面對攝影機
            }
            FaceTarget();
        }
        if (attackDistence <= attackRaduis && attackCD>attackRate)
        {
            attackCD = 0;
            MonsterAttack();
        }
        attackCD += Time.deltaTime;
    }
    void FaceTarget()
    {
        //Vector3 direction = (target.position - transform.position).normalized;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime *5f);
        
        //transform.rotation = Quaternion.Euler(Vector3.zero);
    }
    void MonsterAttack()
    {
        GameObject monsterAttackArea =  Instantiate(attackCylinder, transform.position, transform.rotation);
        Destroy(monsterAttackArea, 0.5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRaduis);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRaduis);
    }
}
