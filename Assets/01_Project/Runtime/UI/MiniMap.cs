using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform player;

    public void Init(Transform player)
    {
        this.player = player;
    }

    private void LateUpdate()
    {
        if (!player)
        {
            return;
        }

        //不要去改到Y軸
        transform.position = player.position.With(y: transform.position.y);

        //地圖如果要可以轉就不要註解底下這行
        //transform.rotation = Quaternion.Euler(90f, player.rotation.y, 0f);
    }
}