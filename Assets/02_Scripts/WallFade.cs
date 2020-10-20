using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    int wall;
    Transform alphaTransform;
    public Transform player;
    public Material[] colors;
    void Start()
    {
        wall = LayerMask.GetMask("Wall");
    }
    void Update()
    {
        float cameraraylength = 100;
        Vector3 direction = (player.position - Camera.main.transform.position).normalized;
        RaycastHit wallrCross;
        if (Physics.Raycast(Camera.main.transform.position, direction, out wallrCross, cameraraylength, wall))
        {
            //Physics.RaycastNonAlloc 陣列raycast
            if (alphaTransform != null)
            {
                if (alphaTransform.GetComponent<MeshRenderer>() != null)
                {
                    alphaTransform.transform.GetComponent<MeshRenderer>().material = colors[1];
                }
            }
            if (wallrCross.transform.CompareTag("Invisible"))
            {
                wallrCross.transform.GetComponent<WallFadeGroup>().AllWallFade();
            }
            alphaTransform = wallrCross.transform;
            alphaTransform.GetComponent<MeshRenderer>().material = colors[0];
            //alphaTransform.GetComponent<MeshRenderer>().material.color = Color.black * 0.4f;
        }
        else if (alphaTransform != null)
        {
            if (wallrCross.transform.CompareTag("Invisible"))
            {
                wallrCross.transform.GetComponent<WallFadeGroup>().AllWallRecover();
            }
            alphaTransform.transform.GetComponent<MeshRenderer>().material = colors[1];
            //alphaTransform.transform.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        Debug.DrawLine(Camera.main.transform.position, player.position, Color.blue);
    }
}
