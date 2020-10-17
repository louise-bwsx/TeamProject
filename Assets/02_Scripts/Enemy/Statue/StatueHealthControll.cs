using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueHealthControll : MonoBehaviour
{
    bool isInvincible = false;
    MonsterHealth monsterHealth;
    void Start()
    {
        monsterHealth = GetComponent<MonsterHealth>();
    }

    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            monsterHealth.enabled = false;
            //isInvincible = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Guard"))
        {
            monsterHealth.enabled = true;
            //isInvincible = false;
        }
    }
}
