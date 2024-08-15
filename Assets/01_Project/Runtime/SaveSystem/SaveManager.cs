using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    private GetHitEffect getHitEffect;
    private SkillBase skillBase;
    public CharacterBase characterBase;

    protected override void OnAwake()
    {
        characterBase = FindObjectOfType<CharacterBase>();
        getHitEffect = FindObjectOfType<GetHitEffect>();
        skillBase = FindObjectOfType<SkillBase>();

        Debug.Log("SaveManager.Start()");
        Debug.Log("讀取使用者設定");
        //TODO: 看能不能改成只讀使用者設定
        CentralData.LoadData();
    }

    public void SaveData(string date)
    {
        Debug.Log("進行存檔");
        CentralData centralData = CentralData.GetInst();
        //魔塵
        centralData.dust = getHitEffect.dust;
        //技能等級
        centralData.fireSkillLevel = skillBase.fireSkillLevel;
        centralData.poisonSkillLevel = skillBase.poisonSkillLevel;
        centralData.stoneSkillLevel = skillBase.stoneSkillLevel;
        centralData.waterSkillLevel = skillBase.waterSkillLevel;
        centralData.windSkillLevel = skillBase.windSkillLevel;
        //角色數值
        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            centralData.charaterStats[i] = characterBase.charaterStats[i];
        }
        CentralData.SaveData();
    }

    public void LoadData(string loadDate)
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

    public void SaveUserSettings()
    {
        Debug.Log("儲存使用者設定 目前只有音樂音效");
        //BGM及音效
        CentralData centralData = CentralData.GetInst();
        centralData.BGMVol = AudioManager.Inst.volumeBGM;
        centralData.SFXVol = AudioManager.Inst.volumeSFX;
        CentralData.SaveData();
    }
}