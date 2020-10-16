using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPoisonSkill : MonoBehaviour
{
    public float DestroyTime = 3F;
    public GameObject poisonEffect;
    public GameObject bomb;
    AudioSource audioSource;
    //public GameObject explosionEffect;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = CentralData.GetInst().SFXVol;
        Destroy(gameObject, DestroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireAttack"))
        {
            Destroy(gameObject);
            Instantiate(bomb, transform.position, transform.rotation);
            //生成爆炸特效
            //音效
            //特效DestroyTime後消失
        }
    }
}