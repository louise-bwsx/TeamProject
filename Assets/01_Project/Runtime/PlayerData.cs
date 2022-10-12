using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float Playerhealth;
    public float maxHealth;
    //位置
    public float[] position;
    //物品
    //裝備欄
    public PlayerData(GetHitEffect playerhealth, Transform playerPosition)
    {
        Playerhealth = playerhealth.playerHealth;
        maxHealth = playerhealth.maxHealth;

        position = new float[3];
        position[0] = playerPosition.position.x;
        position[1] = playerPosition.position.y;
        position[2] = playerPosition.position.z;
    }
}
