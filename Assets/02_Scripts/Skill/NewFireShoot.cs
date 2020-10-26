using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFireShoot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skill") || other.CompareTag("Wall") )
        {
            Destroy(gameObject);
        }
    }
}
