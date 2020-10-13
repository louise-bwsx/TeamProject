using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWaterSkill : MonoBehaviour
{
    public float explodeRadius = 5f;
    public float force = 700f;
    public float countdown = 1f;
    public AudioSource audioSource;//音樂放置
    public AudioClip SFX;//音效
    void Update()
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, explodeRadius);
            }
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster")|| other.CompareTag("Wall")|| other.CompareTag("Skill"))
        {
            Explode();
        }
    }
}
