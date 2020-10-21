using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandGround : MonoBehaviour
{
    RaycastHit hit;
    Vector3 originTransform;
    LayerMask floor;

    void Start()
    {
        floor = LayerMask.GetMask("Floor");
    }
    void LateUpdate()
    {
        Debug.DrawLine(transform.position, transform.position - transform.up * 100, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10f, floor))
        {
            originTransform = transform.position;
            transform.position = hit.point + transform.up * 0.4f;
        }
        else
        {
            //待測試看起來ok
            transform.position = originTransform;
            //迴避穿地板
            //transform.position.Set(transform.position.x, originTransform.y, transform.position.z);
        }
    }
}
