using UnityEngine;
using System.IO;

public class CentralData
{
    private static CentralData inst = null;//static 具有唯一性 全域變數
    private static string path = Application.dataPath + "/Playerdata/";
    private static string filename = "Record.json";

    public static CentralData GetInst()
    {
        //判斷是否有中繼點
        if (inst == null)
        {
            Debug.Log("inst == null");
            if (File.Exists(path + filename))
            {
                LoadData();
                //讀取中繼點
                //inst = LoadData();
            }
            else
            {
                //新增一個中繼點
                inst = new CentralData();
            }
        }
        return inst;
    }

    public void SaveData()
    {
        if (inst == null)
        {
            Debug.Log("存檔時inst == null");
            return;
        }
        string jsonString = JsonUtility.ToJson(inst);
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + filename, jsonString);
        Debug.Log("存檔成功");
    }

    public static CentralData LoadData()
    {
        if (!File.Exists(path + filename))
        {
            CreateData();
        }
        string jsonString = File.ReadAllText(path + filename);
        Debug.Log(jsonString);
        inst = JsonUtility.FromJson<CentralData>(jsonString);
        Debug.Log("讀檔成功");
        return inst;
    }

    private static void CreateData()
    {
        Debug.Log("找不到存檔 新增一個");
        Directory.CreateDirectory(path);
        //jsonString = JsonUtility.ToJson(new CentralData());
        inst = new CentralData();
        string jsonString = JsonUtility.ToJson(inst);
        File.WriteAllText(path + filename, jsonString);
    }

    //BGM及音效
    public float BGMVol = 0.5f;//音量0~1
    public float SFXVol = 0.5f;
    //魔塵
    public int dust = 100000;
    //技能等級
    public int fireSkillLevel;
    public int poisonSkillLevel;
    public int stoneSkillLevel;
    public int waterSkillLevel;
    public int windSkillLevel;
    //角色數值
    public int[] charaterStats = new int[5];

    //建構子
    private CentralData()
    {

    }

    public CentralData(
        float bGMVol,
        float sFXVol,
        int dust,
        int fireSkillLevel,
        int poisonSkillLevel,
        int stoneSkillLevel,
        int waterSkillLevel,
        int windSkillLevel)
    {
        BGMVol = bGMVol;
        SFXVol = sFXVol;
        this.dust = dust;
        this.fireSkillLevel = fireSkillLevel;
        this.poisonSkillLevel = poisonSkillLevel;
        this.stoneSkillLevel = stoneSkillLevel;
        this.waterSkillLevel = waterSkillLevel;
        this.windSkillLevel = windSkillLevel;
        this.charaterStats = new int[5];
    }
}
