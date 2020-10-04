using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LongRangeEnemyController : MonoBehaviour
{
    public float lookRaduis = 10;
    public float attackRaduis = 5;//遠攻距離
    public Transform bow;
    public GameObject arrow;
    //public Transform monsterShootingRotation;
    public Transform shootingposition;
    public float attackCD;
    public float attackRate = 2;
    public float force = 1500;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distence = Vector3.Distance(target.position, transform.position);
        float attackDistence = Vector3.Distance(target.position, transform.position);
        if (distence <= lookRaduis)
        {
            //向著target走
            agent.SetDestination(target.position);
            //面對攝影機
            transform.rotation = Quaternion.Euler(Vector3.zero);
            //弓面對Target
            WeaponFaceTarget();
            //如果攻擊距離小於攻擊範圍 且 CD時間到
            if (attackDistence <= attackRaduis && attackCD > attackRate)
            {
                MonsterAttack();
            }
        }
        attackCD += Time.deltaTime;
    }
    void WeaponFaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        bow.rotation = Quaternion.Slerp(bow.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void MonsterAttack()
    {
        //生成
        GameObject shootingArrow = Instantiate(arrow, shootingposition.position, bow.rotation);
        //射出
        shootingArrow.GetComponent<Rigidbody>().AddForce(bow.forward * force);
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
