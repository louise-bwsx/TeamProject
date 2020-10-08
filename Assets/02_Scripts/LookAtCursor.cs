using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCursor : MonoBehaviour
{
    int floor;
    Rigidbody RD;
    public Vector3 playertomouse;
    public Quaternion rotationangle;
    void Start()
    {
        floor = LayerMask.GetMask("Floor");
        RD = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //設定當我按下左鍵才會向滑鼠方向做出左鍵的相對應動作
        float cameraraylength = 100;
        Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorcross;
        if (Physics.Raycast(cameraray, out floorcross, cameraraylength, floor))
        {
            playertomouse = floorcross.point - transform.position;
            playertomouse.y = 0;
            rotationangle = Quaternion.LookRotation(playertomouse);
            RD.MoveRotation(rotationangle);
        }
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
