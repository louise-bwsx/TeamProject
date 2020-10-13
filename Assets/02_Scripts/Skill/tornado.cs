using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tornado : MonoBehaviour
{
    public float destroyTime = 3F;
    float countdown;
    public AudioSource audioSource;//音樂放置
    public AudioClip SFX;//音效
    public GameObject explosionObject;//龍捲風物件生成
    public GameObject explosionEffect;
    void Start()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            audioSource.PlayOneShot(SFX);
            Explode();
        }
    }
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject, destroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireAttack"))
        {
            Destroy(gameObject,0.3f);
           Instantiate(explosionObject, transform.position, transform.rotation);
      
        }
    }

}
