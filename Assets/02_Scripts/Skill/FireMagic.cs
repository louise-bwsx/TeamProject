using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    public float DestroyTime = 3F;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poison")|| other.CompareTag("Tornado")|| other.CompareTag("Skill")||other.CompareTag("Monster")|| other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
