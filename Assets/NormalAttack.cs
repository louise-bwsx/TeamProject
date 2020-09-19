using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public GameObject AttackRange;

    void Start()
    {
        AttackRange.GetComponent<MeshRenderer>().enabled = false;
        AttackRange.GetComponent<Collider>().enabled = false;
    }
    public void Attack0Start()
    { 
        
    }
    public void Attack1Start()
    {
        AttackRange.GetComponent<MeshRenderer>().enabled = true;
        AttackRange.GetComponent<Collider>().enabled = true;
    }
    public void Attack0End()
    {

    }
    public void Attack1End()
    {
        AttackRange.GetComponent<MeshRenderer>().enabled = false;
        AttackRange.GetComponent<Collider>().enabled = false;
    }
}
