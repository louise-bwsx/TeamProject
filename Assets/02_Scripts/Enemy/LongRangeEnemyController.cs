using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LongRangeEnemyController : EnemyController
{
    public Transform bow;
    public GameObject arrow;
    public Transform shootingposition;
    public Transform monsterPlane;
    public float force = 1500;
    MonsterHealth monsterHealth;
    void Start()
    {
        monsterHealth = GetComponent<MonsterHealth>();
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
    }
    void Update()
    {
        float distence = Vector3.Distance(target.position, transform.position);
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
            if (distence <= attackRaduis)
            {
                if (attackCD > attackRate)
                {
                    attackCD = 0;
                    monsterHealth.animator.SetTrigger("Attack");
                }
                if (monsterHealth.animator.GetBool("IsAttack"))
                {
                    MonsterAttack();
                }
            }
        }
        else if (distence >= lookRaduis)
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
}
