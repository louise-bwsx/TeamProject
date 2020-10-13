using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    int floor;
    Vector3 playertomouse;
    void Start()
    {
        floor = LayerMask.GetMask("Floor");
    }
    void Update()
    {
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
