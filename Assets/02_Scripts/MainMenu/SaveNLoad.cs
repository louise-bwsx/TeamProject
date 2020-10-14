using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad: MonoBehaviour
{
    GetHitEffect getHitEffect;
    SkillBase skillBase;
    void Start()
    {
        getHitEffect = FindObjectOfType<GetHitEffect>();
        skillBase = FindObjectOfType<SkillBase>();
    }
    public void SaveData()
    {
        Debug.Log("進行存檔");
        CentralData.GetInst().dust = getHitEffect.dust;

        CentralData.GetInst().fireSkillLevel = skillBase.fireSkillLevel;
        CentralData.GetInst().poisonSkillLevel = skillBase.poisonSkillLevel;
        CentralData.GetInst().stoneSkillLevel = skillBase.stoneSkillLevel;
        CentralData.GetInst().waterSkillLevel = skillBase.waterSkillLevel;
        CentralData.GetInst().windSkillLevel = skillBase.windSkillLevel;
        CentralData.SaveData();
    }
    public void LoadData()
    {
        Debug.Log("進行讀檔");
        getHitEffect.dust = CentralData.GetInst().dust;

        skillBase.fireSkillLevel = CentralData.GetInst().fireSkillLevel;
        skillBase.poisonSkillLevel = CentralData.GetInst().poisonSkillLevel;
        skillBase.stoneSkillLevel = CentralData.GetInst().stoneSkillLevel;
        skillBase.waterSkillLevel = CentralData.GetInst().waterSkillLevel;
        skillBase.windSkillLevel = CentralData.GetInst().windSkillLevel;

        CentralData.LoadData();
    }
}
