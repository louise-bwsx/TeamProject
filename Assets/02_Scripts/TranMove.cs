using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TranMove : MonoBehaviour
{
    public Transform pos;

   void OnTriggerEnter(Collider obj)
    {
        if ( obj.tag== "Player")
        {
            obj.transform.position = pos.position;
        }
    }
}
