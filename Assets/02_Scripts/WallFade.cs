using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    int wall;
    int playerLayer;
    Transform alphaTransform;
    public Transform player;
    void Start()
    {
        wall = LayerMask.GetMask("Wall");
        playerLayer = LayerMask.GetMask("Player");
    }
    void Update()
    {
        float cameraraylength = 100;
        Vector3 direction = (player.position - Camera.main.transform.position).normalized;
        RaycastHit wallrCross;
        if (Physics.Raycast(Camera.main.transform.position,direction ,out wallrCross,cameraraylength,wall))
        {
            //Physics.RaycastNonAlloc 陣列raycast
            if(alphaTransform != null)
                alphaTransform.transform.GetComponent<MeshRenderer>().material.color = Color.black;
            alphaTransform = wallrCross.transform;
            alphaTransform.GetComponent<MeshRenderer>().material.color = Color.black * 0.4f;
        }
        else if (alphaTransform != null)
        {
            alphaTransform.transform.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        Debug.DrawLine(Camera.main.transform.position, player.position, Color.blue);
    }
}
