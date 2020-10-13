﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPoisonSkill : MonoBehaviour
{
    public float DestroyTime = 3F;
    public GameObject poisonEffect;
    //public GameObject explosionEffect;
    void Start()
    {
        GameObject UsingSkill = Instantiate(poisonEffect, transform.position, transform.rotation);
        Destroy(gameObject, DestroyTime);
        Destroy(UsingSkill, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireAttack"))
        {
            //生成爆炸特效
            //音效
            //特效DestroyTime後消失
        }
    }
 





}