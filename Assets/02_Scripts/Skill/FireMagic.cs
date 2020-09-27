using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagic : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;
   public float DestroyTime = 3F;
    float countdown;
    bool hasExplode = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExplode)
        {
            Explode();
            hasExplode = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Explode()
    {

        Destroy(gameObject, DestroyTime);
    }
}
