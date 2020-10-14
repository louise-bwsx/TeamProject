using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public float lookRaduis = 10;
    public float attackRaduis = 1;//近戰距離
    public Transform monsterOriginPos;
    public float attackCD;
    public float attackRate = 1;
    protected Transform target;
    protected NavMeshAgent agent;
    protected float distence;
    protected Animator animator;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRaduis);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRaduis);
    }
}
