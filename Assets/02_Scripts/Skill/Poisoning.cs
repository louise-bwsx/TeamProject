using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoning : MonoBehaviour
{


    //public float force = 700f;
    public float DestroyTime = 3F;
    float countdown;
    bool hasExplode = false;

    public AudioSource GunAudio;//音樂放置
    public AudioClip SoilWallSFX;//毒音效
    public GameObject explosionObject;//爆炸物件生成
    //public GameObject explosionEffect;//放置特效



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

        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FirMagic"))
        {

            Destroy(gameObject, 0.3f);
            Instantiate(explosionObject, transform.position, transform.rotation);
        }
     


    }
 





}