﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoor : MonoBehaviour
{
    public GameObject[] doorClose;
    public GameObject[] door;
    public GameObject aa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
       
        
    }
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
            aa.SetActive(true);
            foreach (GameObject h in door)
            {
                h.SetActive(false);
                h.GetComponent<MeshRenderer>().enabled = false;
            }

        }

        }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(1);

            foreach (GameObject i in doorClose)
            {
                Debug.Log(2);
                i.SetActive(true);
            }
        }
    }

}
