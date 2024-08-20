using System.Collections.Generic;

public class GameSaveData
{
    public string time;
    //魔塵
    public int dust = 0;
    //角色數值
    public int[] charaterStats = new int[5];
    //技能等級
    public int[] skillLevels = new int[5];

    public float playerHp = 0;
    public float playerMaxHp = 100;

    public float playerStamina = 0;
    public float playerMaxStamina = 100;
    public Dictionary<string, TransformSave> transformSaves = new Dictionary<string, TransformSave>();
}