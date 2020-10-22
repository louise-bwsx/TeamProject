using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandGround : MonoBehaviour
{
    RaycastHit hit;
    Vector3 originTransform;
    LayerMask floor;
    LayerMask cantMove;

    void Start()
    {
        floor = LayerMask.GetMask("Floor");
        cantMove = LayerMask.GetMask("CantMove");
    }
    void LateUpdate()
    {
        Debug.DrawLine(transform.position, transform.position - transform.up * 100, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, 10f, floor))
        {
            originTransform = transform.position;
            transform.position = hit.point + transform.up * 0.4f;
        }
        else if (hit.transform.CompareTag("CantMove"))
        {
            //待測試看起來ok
            transform.position = originTransform;
        }
        else
        {
            transform.position = originTransform;
        }
    }
}
