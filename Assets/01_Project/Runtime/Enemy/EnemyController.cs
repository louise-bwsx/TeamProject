using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{
    public float longRangeRadius = 10;//遠攻距離
    public float meleeRadius = 1;//近戰距離
    public float detectRadius = 15;
    protected float distence;

    public float attackCD;
    public float attackRate = 1;

    protected Transform target;
    protected NavMeshAgent agent;
    protected Animator animator;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, longRangeRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, meleeRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
