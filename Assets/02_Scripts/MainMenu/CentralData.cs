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
    {   //取得存檔路徑的檔名

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
        CentralData data = GetInst();
        //取得存檔的路徑與檔名

        //CentralData 轉Json
        string jstr = JsonUtility.ToJson(mInstance);
        //寫檔
        if (!Directory.Exists(path))//不存在
        {   //建立新的資料夾
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + filename, jstr);
    }

    public static CentralData LoadData()
    {

        string jstr = File.ReadAllText(path + filename);

        CentralData data = JsonUtility.FromJson<CentralData>(jstr);
        return data;
    }

    //成員變數
    public float BGMVol = 1.0f;//音量0~1
    public float SFXVol = 1.0f;


    //建構子
    private CentralData()
    {

    }
    //方法們

}
