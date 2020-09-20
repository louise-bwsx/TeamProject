using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    public GameObject AttackCube;
    public Collider SwordCube;

    void Start()
    {
        SwordCube.enabled = false;
        AttackCube.SetActive(false);
    }
    public void Attack0Start()
    {
        SwordCube.enabled = true;
    }
    public void Attack1Start()
    {
        AttackCube.SetActive(true);
    }
    public void Attack0End()
    {
        SwordCube.enabled = false;
    }
    public void Attack1End()
    {
        AttackCube.SetActive(false);
    }
}
