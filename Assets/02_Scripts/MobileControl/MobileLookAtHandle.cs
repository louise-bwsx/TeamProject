using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileLookAtHandle : MonoBehaviour
{
    public GameObject handle;
    void Update()
    {
        transform.LookAt(handle.transform);
    }
}
