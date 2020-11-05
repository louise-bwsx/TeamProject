using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileLookAtHandle : MonoBehaviour
{
    public Transform handle;
    public Transform rollDirection;
    void Update()
    {
        //Debug.Log(Quaternion.FromToRotation(Vector3.up,handle.transform.position - transform.position).eulerAngles);
        //被改的Transform.localRotation = Quaternion.Euler(0,                       (被改的軸向, 兩者相減取角度).得到的Z軸,0)
        rollDirection.localRotation = Quaternion.Euler(0, -Quaternion.FromToRotation(Vector3.up, handle.transform.position - transform.position).eulerAngles.z, 0);
    }
}
