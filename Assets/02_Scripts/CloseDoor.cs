﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject[] doorClose;
    public GameObject invisibleWall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject i in doorClose)
            {
                i.SetActive(false);
            }
        }
        if (other.CompareTag("Player"))
        {
            invisibleWall.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GameObject i in doorClose)
            {
                i.SetActive(true);
            }
        }
    }
}
