using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Click;
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.volume = CentralData.GetInst().SFXVol;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(Click);
        }
        if (Input.GetMouseButtonUp(0))
        {
            audioSource.PlayOneShot(Click);
        }
    }
}
