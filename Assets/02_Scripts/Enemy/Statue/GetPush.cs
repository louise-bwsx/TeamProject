using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPush : MonoBehaviour
{
    public GameObject statue;
    Rigidbody RD;
    void Start()
    {
        RD = statue.GetComponent<Rigidbody>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterAttack"))
        {
            RD.isKinematic = false;
        }
    }
}
