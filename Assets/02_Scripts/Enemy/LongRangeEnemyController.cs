using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LongRangeEnemyController : EnemyController
{
    public Transform bow;
    public GameObject arrow;
    public Transform shootingposition;
    public float force = 1500;
    public MonsterHealth monsterHealth;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
    }
    void Update()
    {
        float distence = Vector3.Distance(target.position, transform.position);
        if (distence <= detectRadius)
        {
            agent.enabled = true;
            //變身怪物動畫播放
            animator.SetBool("IsDetect", true);
            //向著target走
            agent.SetDestination(target.position);
            //面對攝影機
            transform.rotation = Quaternion.Euler(Vector3.zero);
            //弓面對Target
            WeaponFaceTarget();
            //如果攻擊距離小於攻擊範圍 且 CD時間到
            if (distence <= longRangeRadius)
            {
                if (attackCD > attackRate)
                {
                    attackCD = 0;
                    animator.SetTrigger("Attack");
                }
            }
        }
        else if (distence >= detectRadius)
        {
            //取消往重生點跑
            agent.enabled = false;
            animator.SetBool("IsDetect", false);
        }

        attackCD += Time.deltaTime;
    }
    void WeaponFaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        bow.rotation = Quaternion.Slerp(bow.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void MonsterAttack()//動畫Event呼叫
    {
        //生成
        GameObject shootingArrow = Instantiate(arrow, shootingposition.position, bow.rotation);
        //射出
        shootingArrow.GetComponent<Rigidbody>().AddForce(bow.forward * force);
        Destroy(shootingArrow, 5f);
    }
}
