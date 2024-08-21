using System;
using UnityEngine;

[Serializable]
public class GameSaveData
{
    public string time;
    //魔塵
    public int dust = 0;
    //角色數值
    public int[] statsLevel = new int[5];
    //技能等級
    public int[] skillLevels = new int[5];

    public float playerHp = 0;
    public float playerMaxHp = 100;

    public float playerStamina = 0;
    public float playerMaxStamina = 100;

    //沒有EasySave 沒辦法存vector3 struct class
    public float posX;
    public float posY;
    public float posZ;
    //不要存Rotation 在讀檔時 會讓攝影機轉錯方向

    public GameSaveData()
    {
    }

    public void SaveTransform(Transform transform)
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(posX, posY, posZ);
    }
}