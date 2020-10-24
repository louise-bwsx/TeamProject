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
    void Start()
    {
        getHitEffect = FindObjectOfType<GetHitEffect>();
        skillBase = FindObjectOfType<SkillBase>();
    }
    public void SaveData(Text dateTime)
    {
        Debug.Log("進行存檔");
        CentralData.SaveData();

        CentralData.GetInst().dust = getHitEffect.dust;

        CentralData.GetInst().fireSkillLevel = skillBase.fireSkillLevel;
        CentralData.GetInst().poisonSkillLevel = skillBase.poisonSkillLevel;
        CentralData.GetInst().stoneSkillLevel = skillBase.stoneSkillLevel;
        CentralData.GetInst().waterSkillLevel = skillBase.waterSkillLevel;
        CentralData.GetInst().windSkillLevel = skillBase.windSkillLevel;

        CentralData.GetInst().BGMVol = audioMgr.BGMSlider.value;
        CentralData.GetInst().SFXVol = audioMgr.SFXSlider.value;

        dateTime.text = DateTime.Now.ToString();
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

        audioMgr.BGMSlider.value = CentralData.GetInst().BGMVol;
        audioMgr.SFXSlider.value = CentralData.GetInst().SFXVol;

        Debug.Log("火技能" + skillBase.fireSkillLevel +
                  "\n毒技能" + skillBase.poisonSkillLevel +
                  "\n土技能" + skillBase.stoneSkillLevel +
                  "\n水技能" + skillBase.waterSkillLevel +
                  "\n風技能" + skillBase.windSkillLevel);
    }
}
