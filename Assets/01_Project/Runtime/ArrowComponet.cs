using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowComponet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //模擬箭釘在牆上的功能但物體消失會浮空
        if (!other.gameObject.CompareTag("Arrow") || !other.gameObject.CompareTag("Broken"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Collider>().isTrigger = true;
        }
    }
}
