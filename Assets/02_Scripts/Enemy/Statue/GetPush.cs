using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPush : MonoBehaviour
{
    public GameObject statue;
    public BossFightRule bossFightRule;
    Rigidbody RD;
    void Start()
    {
        bossFightRule = FindObjectOfType<BossFightRule>();
        RD = statue.GetComponent<Rigidbody>();    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterAttack") && bossFightRule.bossFightState == 2)
        {
            RD.isKinematic = false;
        }
    }
}
