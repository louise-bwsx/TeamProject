using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoSingleton<SaveManager>
{
    GetHitEffect getHitEffect;
    SkillBase skillBase;
    public AudioManager audioMgr;
    public CharacterBase characterBase;
    private void Start()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        getHitEffect = FindObjectOfType<GetHitEffect>();
        skillBase = FindObjectOfType<SkillBase>();

        Debug.Log("SaveManager.Start()");
        LoadUserSettings();
    }
    public void SaveData(Text dateTime)
    {
        Debug.Log("進行存檔");
        //魔塵
        CentralData.GetInst().dust = getHitEffect.dust;
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
        CentralData.GetInst().SaveData();
    }
    public void LoadData()
    {
        Debug.Log("進行讀檔");
        CentralData centralData = CentralData.LoadData();
        //魔塵
        getHitEffect.dust = centralData.dust;
        //技能等級
        skillBase.fireSkillLevel = centralData.fireSkillLevel;
        skillBase.poisonSkillLevel = centralData.poisonSkillLevel;
        skillBase.stoneSkillLevel = centralData.stoneSkillLevel;
        skillBase.waterSkillLevel = centralData.waterSkillLevel;
        skillBase.windSkillLevel = centralData.windSkillLevel;
        //TODO: 之後改在SkillWindow
        //skillBase.SkillImageChange(skillBase.fireSkillLevel, skillBase.fireImage);
        //skillBase.SkillImageChange(skillBase.poisonSkillLevel, skillBase.poisonImage);
        //skillBase.SkillImageChange(skillBase.stoneSkillLevel, skillBase.stoneImage);
        //skillBase.SkillImageChange(skillBase.waterSkillLevel, skillBase.waterImage);
        //skillBase.SkillImageChange(skillBase.windSkillLevel, skillBase.windImage);
        //角色數值
        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            characterBase.charaterStats[i] = centralData.charaterStats[i];
        }
        characterBase.StatsCheck();
        Debug.Log("火技能" + skillBase.fireSkillLevel +
                  "\n毒技能" + skillBase.poisonSkillLevel +
                  "\n土技能" + skillBase.stoneSkillLevel +
                  "\n水技能" + skillBase.waterSkillLevel +
                  "\n風技能" + skillBase.windSkillLevel);
    }

    private void LoadUserSettings()
    {
        Debug.Log("讀取使用者設定");
        //TODO: 看能不能改成只讀使用者設定
        CentralData centralData = CentralData.LoadData();
        //BGM及音效
        audioMgr.Load(centralData.BGMVol, centralData.SFXVol);
    }

    public void SaveUserSettings()
    {
        Debug.Log("儲存使用者設定 目前只有音樂音效");
        //BGM及音效
        CentralData centralData = CentralData.GetInst();
        centralData.BGMVol = audioMgr.volumeBGM;
        centralData.SFXVol = audioMgr.volumeSFX;
        centralData.SaveData();
    }
}
