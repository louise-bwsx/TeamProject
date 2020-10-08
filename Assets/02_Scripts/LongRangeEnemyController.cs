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
    public Transform monsterPlane;
    public float attackCD;
    public float attackRate = 2;
    public float force = 1500;
    MonsterHealth monsterHealth;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        monsterHealth = GetComponent<MonsterHealth>();
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
            agent.enabled = true;
            //變身怪物動畫播放
            monsterHealth.animator.SetBool("IsDetect", true);
            //向著target走
            agent.SetDestination(target.position);
            //面對攝影機
            transform.rotation = Quaternion.Euler(Vector3.zero);
            //弓面對Target
            WeaponFaceTarget();
            //如果攻擊距離小於攻擊範圍 且 CD時間到
            if (attackDistence <= attackRaduis)
            {
                if (attackCD > attackRate)
                {
                    attackCD = 0;
                    monsterHealth.animator.SetTrigger("Attack");
                }
            }
                if (monsterHealth.animator.GetBool("IsAttack"))
                {
                    MonsterAttack();
                }
        }
        else if (distence >= lookRaduis/* && monsterPlane!=null*/)
        {
            //取消往重生點跑
            agent.enabled = false;
            monsterHealth.animator.SetBool("IsDetect", false);
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
        monsterHealth.animator.SetBool("IsAttack", false);
        Destroy(shootingArrow, 5f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRaduis);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRaduis);
    }


}
