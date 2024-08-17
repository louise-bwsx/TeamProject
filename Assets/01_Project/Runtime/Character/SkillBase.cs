using UnityEngine;
using UnityEngine.UI;

public enum enumSkill
{
    fireSkill,
    poisonSkill,
    stoneSkill,
    waterSkill,
    windSkill
}

public class SkillBase : MonoBehaviour
{
    //等級
    public int skillLevelNeed = 1000;
    public int[] skillType = new int[5];

    public int fireSkillLevel = 0;
    public int poisonSkillLevel = 0;
    public int stoneSkillLevel = 0;
    public int waterSkillLevel = 0;
    public int windSkillLevel = 0;

    PlayerStats mobileStats;
    public Image[] fireImage;
    public Image[] poisonImage;
    public Image[] stoneImage;
    public Image[] waterImage;
    public Image[] windImage;
    int skillMaxLevel = 5;
}