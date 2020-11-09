using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveNLoad: MonoBehaviour
{
    MobileStats mobileStats;
    SkillBase skillBase;
    public AudioMgr audioMgr;
    public CharacterBase characterBase;
    void Start()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        mobileStats = FindObjectOfType<MobileStats>();
        skillBase = FindObjectOfType<SkillBase>();
    }
    public void SaveData(Text dateTime)
    {
        Debug.Log("進行存檔");
        //BGM及音效
        CentralData.GetInst().BGMVol = audioMgr.BGMSlider.value;
        CentralData.GetInst().SFXVol = audioMgr.SFXSlider.value;
        //魔塵
        CentralData.GetInst().dust = mobileStats.dust;
        //技能等級
        CentralData.GetInst().fireSkillLevel = skillBase.fireSkillLevel;
        CentralData.GetInst().poisonSkillLevel = skillBase.poisonSkillLevel;
        CentralData.GetInst().stoneSkillLevel = skillBase.stoneSkillLevel;
        CentralData.GetInst().waterSkillLevel = skillBase.waterSkillLevel;
        CentralData.GetInst().windSkillLevel = skillBase.windSkillLevel;
        //角色數值
        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            CentralData.GetInst().charaterStats[i] = characterBase.charaterStats[i];
        }
        dateTime.text = DateTime.Now.ToString();
        CentralData.SaveData();
    }
    public void LoadData()
    {
        Debug.Log("進行讀檔");
        CentralData.LoadData();
        //BGM及音效
        audioMgr.BGMSlider.value = CentralData.GetInst().BGMVol;
        audioMgr.SFXSlider.value = CentralData.GetInst().SFXVol;
        //魔塵
        mobileStats.dust = CentralData.GetInst().dust;
        //技能等級
        skillBase.fireSkillLevel = CentralData.GetInst().fireSkillLevel;
        skillBase.poisonSkillLevel = CentralData.GetInst().poisonSkillLevel;
        skillBase.stoneSkillLevel = CentralData.GetInst().stoneSkillLevel;
        skillBase.waterSkillLevel = CentralData.GetInst().waterSkillLevel;
        skillBase.windSkillLevel = CentralData.GetInst().windSkillLevel;
        skillBase.SkillImageChange(skillBase.fireSkillLevel, skillBase.fireImage);
        skillBase.SkillImageChange(skillBase.poisonSkillLevel, skillBase.poisonImage);
        skillBase.SkillImageChange(skillBase.stoneSkillLevel, skillBase.stoneImage);
        skillBase.SkillImageChange(skillBase.waterSkillLevel, skillBase.waterImage);
        skillBase.SkillImageChange(skillBase.windSkillLevel, skillBase.windImage);
        //角色數值
        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            characterBase.charaterStats[i] = CentralData.GetInst().charaterStats[i];
        }
        characterBase.StatsCheck();
        Debug.Log("火技能" + skillBase.fireSkillLevel +
                  "\n毒技能" + skillBase.poisonSkillLevel +
                  "\n土技能" + skillBase.stoneSkillLevel +
                  "\n水技能" + skillBase.waterSkillLevel +
                  "\n風技能" + skillBase.windSkillLevel);
    }
}
