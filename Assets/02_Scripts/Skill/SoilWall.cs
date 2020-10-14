using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilWall : MonoBehaviour
{
    public float DestroyTime = 3F;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = CentralData.GetInst().SFXVol;
        Destroy(gameObject, DestroyTime);
    }

}
