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
        //不穩定完全不知道怎麼觸發的
        //Ray cameraray = Camera.main.ScreenPointToRay(player.position);
        //只觸發滑鼠
        //Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //型態不一樣 一個Ray 一個 Vector3
        //Ray cameraray = Camera.main.ScreenToWorldPoint(player.position);
        Vector3 direction = (player.position - Camera.main.transform.position).normalized;
        RaycastHit wallrCross;
        if (Physics.Raycast(Camera.main.transform.position,direction ,out wallrCross,cameraraylength,wall))
        {
            alphaTransform = wallrCross.transform;
            alphaTransform.GetComponent<MeshRenderer>().material.color = Color.black * 0.5f;
        }
        else if (alphaTransform != null && Physics.Raycast(Camera.main.transform.position, direction, out wallrCross,cameraraylength,playerLayer))
        {
            alphaTransform.transform.GetComponent<MeshRenderer>().material.color = Color.black;
        }
        Debug.DrawLine(Camera.main.transform.position, player.position, Color.blue);
    }
}
