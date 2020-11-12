using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandGround : MonoBehaviour
{
    RaycastHit hit;
    Vector3 originTransform;
    LayerMask floor;
    //ProjectSettings Physics floor跟player交界取消勾選 就可以解決上坡會卡頓的問題
    //角色Rigidbody的Interpolate的下拉式選單改成interpolate就可以把EquipmentGroup就可以刪掉
    //原因是EquipmentGroup裡的物件Rigidbody的Interpolate選項是Extrapolate會影響其他物件
    //Interpolate和Extrapolate 最好只讓玩家使用就好
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
        }
    }
}
