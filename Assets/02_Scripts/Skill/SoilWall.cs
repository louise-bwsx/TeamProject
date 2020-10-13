using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilWall : MonoBehaviour
{
    public float DestroyTime = 3F;
    float countdown;
    public AudioSource GunAudio;//音樂放置
    public AudioClip SFX;//音效
    //public GameObject explosionEffect;//放置特效
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            GunAudio.PlayOneShot(SFX);
            Destroy(gameObject, DestroyTime);
        }
    }

}
