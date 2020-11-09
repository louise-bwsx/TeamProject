using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //血量
    public float Playerhealth;
    //位置
    public float[] position;
    //物品
    //裝備欄
    public PlayerData(MobileStats mobileStats, Transform playerPosition)
    {
        Playerhealth = mobileStats.hp;

        position = new float[3];
        position[0] = playerPosition.position.x;
        position[1] = playerPosition.position.y;
        position[2] = playerPosition.position.z;
    }
}
