﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilWall : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
    public float DestroyTime = 3F;
    float countdown;
    bool hasExplode = false;
    public AudioSource GunAudio;//音樂放置
    public AudioClip SoilWallSFX;//音效
    // Start is called before the first frame update
    void Start()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExplode)
        {
            GunAudio.PlayOneShot(SoilWallSFX);
            Explode();
            hasExplode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Explode()
    {
        //Instantiate(explosionEffect, transform.position, transform.rotation);
        //Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        //foreach (Collider nearbyObject in colliders)
        //{
        //    Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
        //    if (rb != null)
        //    {
        //        rb.AddExplosionForce(force, transform.position, radius);
        //    }
        //}
        Destroy(gameObject, DestroyTime);
    }

}
