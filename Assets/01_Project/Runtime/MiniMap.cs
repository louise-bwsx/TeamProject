using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        //設定新的相機位置為角色的位置
        Vector3 newPosition = player.position;
        //但y軸不變
        newPosition.y = transform.position.y;
        //再把設定好的相機位置存進小地圖相機位置
        transform.position = newPosition;

        //地圖如果要可以轉就不要註解底下這行
        //transform.rotation = Quaternion.Euler(90f, player.rotation.y, 0f);
    }
}
