using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CentralData
{
    private static CentralData mInstance = null;//static 具有唯一性 全域變數
    private static string path = Application.dataPath + "/playerdata/";
    private static string filename = "record.json";
    public static CentralData GetInst()//靜態成員與GetInst()方法
    {   
        //判斷是否有中繼點
        if (mInstance == null)
        {
            if (File.Exists(path + filename))
            {   //讀取中繼點
                mInstance = LoadData();
            }
            else
            {   //新增一個中繼點
                mInstance = new CentralData();
            }
        }
        return mInstance;
    }

    public static void SaveData()
    {
        //CentralData data = GetInst();
        //取得存檔的路徑與檔名

        //CentralData 轉Json
        string jstr = JsonUtility.ToJson(mInstance);
        //寫檔
        if (!Directory.Exists(path))//不存在
        {   //建立新的資料夾
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + filename, jstr);
        Debug.Log("存檔成功");
    }

    public static CentralData LoadData()
    {
        string jstr = File.ReadAllText(path + filename);

        CentralData data = JsonUtility.FromJson<CentralData>(jstr);
        Debug.Log("讀檔成功");
        return data;
    }

    //成員變數
    public float BGMVol = 1;//音量0~1
    public float SFXVol = 1;
    
    //魔塵
    public int dust = 100000;

    //技能等級
    public int fireSkillLevel;
    public int poisonSkillLevel;
    public int stoneSkillLevel;
    public int waterSkillLevel;
    public int windSkillLevel;

    //建構子
    private CentralData()
    {

    }
    //方法們

}
