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
//土炮二維陣列
[System.Serializable]
public class aa
{
    public Image image;
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
    //土炮二維陣列
    public aa[] aaa;


    void Start()
    {
        mobileStats = FindObjectOfType<PlayerStats>();
        fireSkillLevel = CentralData.GetInst().fireSkillLevel;
        poisonSkillLevel = CentralData.GetInst().poisonSkillLevel;
        stoneSkillLevel = CentralData.GetInst().stoneSkillLevel;
        waterSkillLevel = CentralData.GetInst().waterSkillLevel;
        windSkillLevel = CentralData.GetInst().windSkillLevel;
        //TODO: 之後改在SkillWindow
        //SkillImageChange(fireSkillLevel, fireImage);
        //SkillImageChange(poisonSkillLevel, poisonImage);
        //SkillImageChange(stoneSkillLevel, stoneImage);
        //SkillImageChange(waterSkillLevel, waterImage);
        //SkillImageChange(windSkillLevel, windImage);
    }

    public void SkillImageChange(int skillLevel, Image[] skillImage)
    {
        for (int i = 0; i < skillMaxLevel; i++)
        {
            skillImage[i].enabled = false;
        }
        for (int i = 0; i < skillLevel; i++)
        {
            skillImage[i].enabled = true;
        }
    }

    public void SkillLevelUp(int skillType)
    {
        switch ((enumSkill)skillType)
        {
            case enumSkill.fireSkill:
                {
                    Debug.Log(mobileStats.dust + " " + fireSkillLevel + " " + skillLevelNeed + " " + 0 + " " + fireSkillLevel + " " + skillMaxLevel);
                    if (mobileStats.dust - fireSkillLevel * skillLevelNeed >= 0 && fireSkillLevel < skillMaxLevel)
                    {
                        fireImage[fireSkillLevel].enabled = true;
                        fireSkillLevel++;
                        mobileStats.dust = mobileStats.dust - fireSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.poisonSkill:
                {
                    if (mobileStats.dust - poisonSkillLevel * skillLevelNeed >= 0 && poisonSkillLevel < skillMaxLevel)
                    {
                        poisonImage[poisonSkillLevel].enabled = true;
                        poisonSkillLevel++;
                        mobileStats.dust = mobileStats.dust - poisonSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.stoneSkill:
                {
                    if (mobileStats.dust - stoneSkillLevel * skillLevelNeed >= 0 && stoneSkillLevel < skillMaxLevel)
                    {
                        stoneImage[stoneSkillLevel].enabled = true;
                        stoneSkillLevel++;
                        mobileStats.dust = mobileStats.dust - stoneSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.waterSkill:
                {
                    if (mobileStats.dust - waterSkillLevel * skillLevelNeed >= 0 && waterSkillLevel < skillMaxLevel)
                    {
                        waterImage[waterSkillLevel].enabled = true;
                        waterSkillLevel++;
                        mobileStats.dust = mobileStats.dust - waterSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.windSkill:
                {
                    if (mobileStats.dust - windSkillLevel * skillLevelNeed >= 0 && windSkillLevel < skillMaxLevel)
                    {
                        windImage[windSkillLevel].enabled = true;
                        windSkillLevel++;
                        mobileStats.dust = mobileStats.dust - windSkillLevel * skillLevelNeed;
                    }
                    break;
                }
        }
    }
}
