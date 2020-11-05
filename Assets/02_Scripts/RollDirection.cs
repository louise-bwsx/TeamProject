using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDirection : MonoBehaviour
{
    public Transform handleRotation;

    void Update()
    {
        transform.Rotate(new Vector3(0, handleRotation.rotation.x, 0)); 
    }
}
