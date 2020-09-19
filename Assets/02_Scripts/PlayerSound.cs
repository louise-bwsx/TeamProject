using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Click;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(Click);
        }
    }
}
