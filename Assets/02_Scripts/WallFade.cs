using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    int wall;
    Transform alphaTransform;
    void Start()
    {
        wall = LayerMask.GetMask("Wall");
    }
    void Update()
    {
        float cameraraylength = 100;
        Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit wallrCross;
        if (Physics.Raycast(cameraray, out wallrCross, cameraraylength,wall))
        {
            alphaTransform = wallrCross.transform;
            alphaTransform.GetComponent<MeshRenderer>().material.color = Color.black * 0.5f;
        }
        else if(alphaTransform!=null)
        {
            alphaTransform.transform.GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }
}
