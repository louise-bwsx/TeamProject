using System.Collections;
using System.Collections.Generic;
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
    public int fireSkillLevel=0;
    public int poisonSkillLevel = 0;
    public int stoneSkillLevel = 0;
    public int waterSkillLevel = 0;
    public int windSkillLevel = 0;
    //攻擊數值
    public int fireSkill = 0;
    public int poisonSkill = 0;
    public int stoneSkill = 0;
    public int waterSkill = 0;
    public int windSkill = 0;

    public GetHitEffect getHitEffect;
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
        fireSkillLevel = CentralData.GetInst().fireSkillLevel;
        poisonSkillLevel = CentralData.GetInst().poisonSkillLevel;
        stoneSkillLevel = CentralData.GetInst().stoneSkillLevel;
        waterSkillLevel = CentralData.GetInst().waterSkillLevel;
        windSkillLevel = CentralData.GetInst().windSkillLevel;
        fireSkill = fireSkillLevel * 20;
        poisonSkill = poisonSkillLevel * 20;
        stoneSkill = stoneSkillLevel * 20;
        waterSkill = waterSkillLevel * 20;
        windSkill = windSkillLevel * 20;
        SkillImageChange(fireSkillLevel, fireImage);
        SkillImageChange(poisonSkillLevel, poisonImage);
        SkillImageChange(stoneSkillLevel, stoneImage);
        SkillImageChange(waterSkillLevel, waterImage);
        SkillImageChange(windSkillLevel, windImage);
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
                    Debug.Log(getHitEffect.dust + " " + fireSkillLevel + " " + skillLevelNeed + " " + 0 + " " + fireSkillLevel + " " + skillMaxLevel);
                    if (getHitEffect.dust - fireSkillLevel * skillLevelNeed >= 0 && fireSkillLevel < skillMaxLevel)
                    {
                        fireImage[fireSkillLevel].enabled = true;
                        fireSkill += 20;
                        fireSkillLevel++;
                        getHitEffect.dust = getHitEffect.dust - fireSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.poisonSkill:
                {
                    if (getHitEffect.dust - poisonSkillLevel * skillLevelNeed >= 0 && poisonSkillLevel < skillMaxLevel)
                    {
                        poisonImage[poisonSkillLevel].enabled = true;
                        poisonSkill += 20;
                        poisonSkillLevel++;
                        getHitEffect.dust = getHitEffect.dust - poisonSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.stoneSkill:
                {
                    if (getHitEffect.dust - stoneSkillLevel * skillLevelNeed >= 0 && stoneSkillLevel < skillMaxLevel)
                    {
                        stoneImage[stoneSkillLevel].enabled = true;
                        stoneSkill += 20;
                        stoneSkillLevel++;
                        getHitEffect.dust = getHitEffect.dust - stoneSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.waterSkill:
                {
                    if (getHitEffect.dust - waterSkillLevel * skillLevelNeed >= 0 && waterSkillLevel < skillMaxLevel)
                    {
                        waterImage[waterSkillLevel].enabled = true;
                        waterSkill += 20;
                        waterSkillLevel++;
                        getHitEffect.dust = getHitEffect.dust - waterSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.windSkill:
                {
                    if (getHitEffect.dust - windSkillLevel * skillLevelNeed >= 0 && windSkillLevel < skillMaxLevel)
                    {
                        windImage[windSkillLevel].enabled = true;
                        windSkill += 20;
                        windSkillLevel++;
                        getHitEffect.dust = getHitEffect.dust - windSkillLevel * skillLevelNeed;
                    }
                    break;
                }
        }
    }
}
