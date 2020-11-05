using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LookAtCursor : MonoBehaviour
{
    int floor;
    public Vector3 playertomouse;
    public Quaternion rotationangle;
    public GetHitEffect getHitEffect;
    void Start()
    {
        floor = LayerMask.GetMask("Floor");
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        float cameraraylength = 500;
        Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorcross;
        if (getHitEffect.playerHealth>0 &&(Physics.Raycast(cameraray, out floorcross, cameraraylength, floor)))
        {
            playertomouse = floorcross.point - transform.position;
            playertomouse.y = 0;
            rotationangle = Quaternion.LookRotation(playertomouse);
            transform.rotation = rotationangle;
        }
            
        //會變成只轉z軸
        //transform.LookAt(Input.mousePosition);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
