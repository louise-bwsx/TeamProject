using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum enumSkill
{
    fireSkill,
    poisonSkill,
    stoneSkil,
    waterSkill,
    windSkill
}
public class SkillBase : MonoBehaviour
{
    public int skillLevelNeed = 1000;
    public int fireSkillLevel=0;
    public int poisonSkillLevel = 0;
    public int stoneSkillLevel = 0;
    public int waterSkillLevel = 0;
    public int windSkillLevel = 0;
    public GetHitEffect getHitEffect;
    public Image[] fireImage;
    public Image[] poisonImage;
    public Image[] stoneImage;
    public Image[] waterImage;
    public Image[] windImage;
    int skillMaxLevel = 5;

    public void SkillLevelUp(int skill)
    {
        switch ((enumSkill)skill)
        {
            case enumSkill.fireSkill:
                {
                    if (getHitEffect.dust - fireSkillLevel * skillLevelNeed >= 0 && fireSkillLevel < skillMaxLevel)
                    {
                        fireImage[fireSkillLevel].enabled = true;
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
                        poisonSkillLevel++;
                        getHitEffect.dust = getHitEffect.dust - poisonSkillLevel * skillLevelNeed;
                    }
                    break;
                }
            case enumSkill.stoneSkil:
                {
                    if (getHitEffect.dust - stoneSkillLevel * skillLevelNeed >= 0 && stoneSkillLevel < skillMaxLevel)
                    {
                        stoneImage[stoneSkillLevel].enabled = true;
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
                        windSkillLevel++;
                        getHitEffect.dust = getHitEffect.dust - windSkillLevel * skillLevelNeed;
                    }
                    break;
                }
        }
    }
}
