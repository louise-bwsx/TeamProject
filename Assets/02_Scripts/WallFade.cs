using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    int wall;
    Vector3 playertomouse;
    void Start()
    {
        wall = LayerMask.GetMask("Wall");
    }
    void Update()
    {
        float cameraraylength = 100;
        Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorcross;
        if (Physics.Raycast(cameraray, out floorcross, cameraraylength, wall))
        {
            //可以 但不知道怎麼調透明度
            floorcross.transform.GetComponent<MeshRenderer>().material.color = Color.clear;
        }
    }
}
