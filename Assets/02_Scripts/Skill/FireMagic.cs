﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    //public float delay = 3f;
    //public float radius = 5f;
    public float force = 700f;
   public float DestroyTime = 3F;
    float countdown;
    bool hasExplode = false;
    public AudioSource GunAudio;//音樂放置
    public AudioClip FireMagicSFX;//音效
  //public GameObject explosionEffect;//放置特效

    // Start is called before the first frame update
    void Start()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExplode)
        {
            GunAudio.PlayOneShot(FireMagicSFX);
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
        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Poison")|| other.CompareTag("tornado")|| other.CompareTag("Skill")||other.CompareTag("Monster")|| other.CompareTag("Wall"))
        {

            Destroy(gameObject);
        }
    }

}
