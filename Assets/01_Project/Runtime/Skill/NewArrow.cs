using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewArrow : MonoBehaviour
{
    public GameObject hitEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skill") || other.CompareTag("Wall"))
        {
            GameObject FX = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(FX,1f);
            //讓毒、風、火龍捲不會因為碰到就消失
            if (!gameObject.CompareTag("WindAttack") && !gameObject.CompareTag("Poison") && !gameObject.CompareTag("Firetornado"))
            { 
                Destroy(gameObject);
            }
        }
    }
}
