using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTornadoSkill : MonoBehaviour
{
    public float destroyTime = 3F;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = CentralData.GetInst().SFXVol;
        Destroy(gameObject, destroyTime);
    }
}
