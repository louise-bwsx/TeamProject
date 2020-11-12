using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSkillPosition : MonoBehaviour
{
    public Transform skillRotation;
    public RectTransform hadle;
    public RectTransform center;

    int floor;
    Vector3 playertomouse;
    MobileSkillChoose mobileSkillChoose;
    MeshRenderer meshRenderer;
    //public MeshRenderer skillRotation;
    RaycastHit wallCross;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mobileSkillChoose = FindObjectOfType<MobileSkillChoose>();
        floor = LayerMask.GetMask("Floor");
    }
    void Update()
    {
        //    //毒水風土火
        //    if (Input.GetMouseButton(2) &&
        //        skillControl.skillList[skillControl.CurIdx + 1].skillTimer > skillControl.skillList[skillControl.CurIdx + 1].skillCD)
        //    {
        //        //毒風土
        //        if (skillControl.CurIdx + 1 == 1 ||
        //           skillControl.CurIdx + 1 == 3 ||
        //           skillControl.CurIdx + 1 == 4)
        //        {
        //            float cameraraylength = 100;
        //            Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //            RaycastHit floorcross;
        //            if (Physics.Raycast(cameraray, out floorcross, cameraraylength, floor))
        //            {
        //                playertomouse = floorcross.point;
        //                playertomouse.y = floorcross.point.y;
        //            }
        //            meshRenderer.enabled = true;
        //            transform.position = playertomouse;
        //        }
        //        //水火
        //        else if (skillControl.CurIdx + 1 == 2 ||
        //                 skillControl.CurIdx + 1 == 5)
        //        {
        //            skillRotation.enabled = true;
        //        }
        //    }
        //    else
        //    {
        //        skillRotation.enabled = false;
        //        meshRenderer.enabled = false;
        //    }
    }
    //project physics floor跟player交界取消勾選 就可以解決上坡會卡頓的問題
    //角色rigidbody interpolate 改interpolate EquipmentGroup就可以刪掉
    //有可能會合作
    //永恆之柱先做戰鬥
    //public void SetRemoteSkillPosition()
    //{
    //    //
    //    if (Physics.Raycast(skillRotation.position, (hadle.position - center.position), out wallCross, Mathf.Infinity, floor))
    //    {

    //    }
    //}
}
