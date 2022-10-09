using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInvincibleEffect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Firetornado") || other.CompareTag("Bomb") )
        {
            Destroy(gameObject);
        }
    }
}
