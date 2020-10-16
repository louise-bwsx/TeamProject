using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : EnemyController
{
    public MeshRenderer meshRenderer;
    public Collider meleeAttackAreacollider;
    public Transform shootingtransform;
    Door door;
    public GameObject arrow;
    public float force = 1500;
    float longRangeAttackCD;
    bool isMeleeAttack;
    bool isLongRangeAttack;
    void Start()
    {
        door = FindObjectOfType<Door>();
        animator = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.player.transform;
    }
    void Update()
    {
        float distence = Vector3.Distance(target.position, transform.position);
        //當門關起來的時候 且 與玩家的距離大於普攻距離
        //if (door.isDoorClose == true)
        //{
            if (distence <= attackRaduis)
            {
                meshRenderer.enabled = true;
                if (isMeleeAttack == false)
                {
                    animator.SetTrigger("WeaponAttack");
                    isMeleeAttack = true;
                }
            }
            else if (distence >= attackRaduis)
            {
                //暫時想不到更好的方法關掉這些東東
                meshRenderer.enabled = false;
                meleeAttackAreacollider.enabled = false;
                //以上關閉近戰攻擊狀態
                shootingtransform.LookAt(target);
                if (isLongRangeAttack == false)
                {
                    animator.SetTrigger("HandAttack");
                    isLongRangeAttack = true;
                }
            }
        //}
    }
    void BossMeleeAttack()//由AnimatorEvent呼叫
    {
        meleeAttackAreacollider.enabled = true;
        meshRenderer.enabled = false;
        isMeleeAttack = false;
    }
    void BossLongRangeAttack()//由AnimatorEvent呼叫
    {
        GameObject shootingArrow = Instantiate(arrow, shootingtransform.position, shootingtransform.rotation);
        shootingArrow.GetComponent<Rigidbody>().AddForce(shootingtransform.forward * force);
        Destroy(shootingArrow, 5f);
        isLongRangeAttack = false;
    }
}
