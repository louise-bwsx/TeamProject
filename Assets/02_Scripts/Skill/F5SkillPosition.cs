using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F5SkillPosition : MonoBehaviour
{
    int floor;
    Vector3 playertomouse;
    public Transform player;
    void Start()
    {
        floor = LayerMask.GetMask("Floor");
    }
    void Update()
    {
        //設定當我按下左鍵才會向滑鼠方向做出左鍵的相對應動作
        float cameraraylength = 100;
        Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorcross;
        if (Physics.Raycast(cameraray, out floorcross, cameraraylength, floor))
        {
            playertomouse = floorcross.point;
            playertomouse.y = 0;
        }
        transform.position = playertomouse;
    }
}
