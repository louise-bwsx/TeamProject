using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteSkillPosition : MonoBehaviour
{
    int floor;
    Vector3 playertomouse;
    SkillControl skillControl;
    MeshRenderer meshRenderer;
    public MeshRenderer skillRotation;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        skillControl = FindObjectOfType<SkillControl>();
        floor = LayerMask.GetMask("Floor");
    }
    void Update()
    {
        //毒水風土火
        if (Input.GetMouseButton(2) &&
            skillControl.skillList[skillControl.CurIdx + 1].lastFireTime > skillControl.skillList[skillControl.CurIdx + 1].fireRate)
        {
            //毒風土
            if (skillControl.CurIdx + 1 == 1 ||
               skillControl.CurIdx + 1 == 3 ||
               skillControl.CurIdx + 1 == 4)
            {
                float cameraraylength = 100;
                Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit floorcross;
                if (Physics.Raycast(cameraray, out floorcross, cameraraylength, floor))
                {
                    playertomouse = floorcross.point;
                    playertomouse.y = floorcross.point.y;
                }
                meshRenderer.enabled = true;
                transform.position = playertomouse;
            }
            //水火
            else if (skillControl.CurIdx + 1 == 2 ||
                     skillControl.CurIdx + 1 == 5)
            {
                skillRotation.enabled = true;
            }
        }
        else
        {
            //skillRotation.enabled = false;
            meshRenderer.enabled = false;
        }
    }
}
