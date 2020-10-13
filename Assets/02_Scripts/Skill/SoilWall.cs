using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilWall : MonoBehaviour
{
    public float DestroyTime = 3F;

    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
