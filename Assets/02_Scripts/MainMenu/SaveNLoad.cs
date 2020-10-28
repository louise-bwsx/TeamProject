using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SaveNLoad: MonoBehaviour
{
    GetHitEffect getHitEffect;
    SkillBase skillBase;
    public AudioMgr audioMgr;
    public CharacterBase characterBase;
    void Start()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        getHitEffect = FindObjectOfType<GetHitEffect>();
        skillBase = FindObjectOfType<SkillBase>();
    }
    public void SaveData(Text dateTime)
    {
        Debug.Log("進行存檔");

        CentralData.GetInst().dust = getHitEffect.dust;

        CentralData.GetInst().fireSkillLevel = skillBase.fireSkillLevel;
        CentralData.GetInst().poisonSkillLevel = skillBase.poisonSkillLevel;
        CentralData.GetInst().stoneSkillLevel = skillBase.stoneSkillLevel;
        CentralData.GetInst().waterSkillLevel = skillBase.waterSkillLevel;
        CentralData.GetInst().windSkillLevel = skillBase.windSkillLevel;

        CentralData.GetInst().BGMVol = audioMgr.BGMSlider.value;
        CentralData.GetInst().SFXVol = audioMgr.SFXSlider.value;
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

        getHitEffect.dust = CentralData.GetInst().dust;

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
        skillBase.fireSkill = skillBase.fireSkillLevel *20;
        skillBase.poisonSkill = skillBase.poisonSkillLevel * 20;
        skillBase.stoneSkill = skillBase.stoneSkillLevel * 20;
        skillBase.waterSkill = skillBase.waterSkillLevel * 20;
        skillBase.windSkill = skillBase.windSkillLevel * 20;

        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            characterBase.charaterStats[i] = CentralData.GetInst().charaterStats[i];
        }
        characterBase.StatsCheck();
        audioMgr.BGMSlider.value = CentralData.GetInst().BGMVol;
        audioMgr.SFXSlider.value = CentralData.GetInst().SFXVol;

        Debug.Log("火技能" + skillBase.fireSkillLevel +
                  "\n毒技能" + skillBase.poisonSkillLevel +
                  "\n土技能" + skillBase.stoneSkillLevel +
                  "\n水技能" + skillBase.waterSkillLevel +
                  "\n風技能" + skillBase.windSkillLevel);
    }
}
