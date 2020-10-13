using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWindSkill : MonoBehaviour
{
    public float DestroyTime = 3F;
    float countdown;
    public AudioSource audioSource;//音樂放置
    public AudioClip SFX;//毒音效
    public GameObject tornado;//爆炸物件生成
    //public GameObject explosionEffect;//放置特效
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            audioSource.PlayOneShot(SFX);
            Destroy(gameObject, DestroyTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireAttack"))
        {
            Destroy(gameObject);
            Instantiate(tornado, transform.position, transform.rotation);
        }
    }
}
