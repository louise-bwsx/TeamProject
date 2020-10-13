using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public PlayerControl playerControl;
    public float staminaCost;
    public float destroyTime = 3F;
    public GameObject skillObject;
    public Transform skillPos;
    public Transform skillRotation;
    public float skillForce = 200f;
    public float lastFireTime;//最後射擊時間
    public float fireRate = 2f;//射擊間隔
    //public ParticleSystem muzzleVFX;//放置粒子物件
    public AudioSource audioSource;//音樂放置
    public AudioClip SFX;//音效
    //public GameObject explosionEffect;//放置特效
    public Image fillImage;//待實作
    void Start()
    {
        lastFireTime = 10f;
        fillImage = transform.Find("CDImage").GetComponent<Image>();
    }
}
