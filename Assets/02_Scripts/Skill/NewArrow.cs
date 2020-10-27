﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewArrow : MonoBehaviour
{
    public GameObject hitEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Skill") || other.CompareTag("Wall") || other.CompareTag("Player"))
        {
            GameObject FX = Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(FX);
            Destroy(gameObject);
        }
    }
}
