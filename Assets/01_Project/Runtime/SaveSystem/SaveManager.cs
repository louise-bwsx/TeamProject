using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    private GameSaveData currentGameSave;
    private SettingsSaveData currentSettingsSave;
    private static string path = Application.dataPath + "/Playerdata/";
    private static string filename = "Save.json";
    private LoadSpace[] loadSpaces;
    private SaveSpace[] saveSpaces;
    private List<TextAsset> saveFiles = new List<TextAsset>();

    public List<ISave> ISaves { get; private set; } = new List<ISave>();//interface沒辦法SerializeField

    protected override void OnAwake()
    {
        Debug.Log("SaveManager.OnAwake()");
        loadSpaces = GetComponentsInChildren<LoadSpace>();
        saveSpaces = GetComponentsInChildren<SaveSpace>();
        RefreshUI();
    }

    public void RefreshUI()
    {
        GetAllSaveFile();
        for (int i = 0; i < loadSpaces.Length; i++)
        {
            if (i > saveFiles.Count - 1)
            {
                loadSpaces[i].Init(null);
                saveSpaces[i].Init(null);
                continue;
            }
            loadSpaces[i].Init(saveFiles[i]);
            saveSpaces[i].Init(saveFiles[i]);
        }
    }

    public void SaveUserSettings()
    {
        Debug.Log("儲存使用者設定 目前只有音樂音效");
        //BGM及音效
        if (currentSettingsSave == null)
        {
            currentSettingsSave = new SettingsSaveData();
        }
        string jsonString = JsonUtility.ToJson(currentSettingsSave);
        File.WriteAllText(path + "Settings" + filename, jsonString);
    }

    public void Save()
    {
        if (currentGameSave == null)
        {
            currentGameSave = new GameSaveData();
        }

        foreach (var save in ISaves)
        {
            save.Save(currentGameSave);
        }

        currentGameSave.time = DateTime.Now.ToString(); ;
        string jsonString = JsonUtility.ToJson(currentGameSave);
        Debug.Log($"jsonString: {jsonString}");
        Debug.Log($"currentGameSave: {currentGameSave}");

        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + filename, jsonString);
        Debug.Log("存檔成功");
    }

    //public static void SavePlayer(GetHitEffect playerhealth, Transform playerPosition)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    string path = Application.persistentDataPath + "/Unity_Project_Save.lui";
    //    FileStream stream = new FileStream(path, FileMode.Create);

    //    PlayerData data = new PlayerData(playerhealth, playerPosition);
    //    formatter.Serialize(stream, data);
    //    stream.Close();
    //}
    //public static PlayerData LoadPlayer()
    //{
    //    string path = Application.persistentDataPath + "/Unity_Project_Save.lui";
    //    if (File.Exists(path))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = new FileStream(path, FileMode.Open);

    //        PlayerData data = formatter.Deserialize(stream) as PlayerData;
    //        stream.Close();
    //        return data;
    //    }
    //    else
    //    {
    //        Debug.LogError(path + "此路徑中找不到任何存檔");
    //        return null;
    //    }
    //}

    public void Load(TextAsset saveFile)
    {
        if (!File.Exists(path + filename))
        {
            Debug.LogError($"!File.Exists Path:{path} FileName:{filename}");
            return;
        }

        Debug.Log($"saveFile.text: {saveFile.text}");
        Debug.Log($"currentGameSave: {currentGameSave}");

        currentGameSave = JsonUtility.FromJson<GameSaveData>(saveFile.text);
        //TODO: 這邊要帶著currentGameSave重新LoadGameScene
        Debug.Log("讀檔成功");
    }

    public GameSaveData GetGameSave()
    {
        //第一次進入 或是 快速進入
        if (currentGameSave == null)
        {
            if (saveFiles.Count < 1)
            {
                currentGameSave = new GameSaveData();
            }
            else
            {
                //已經有根據最後修改時間排列
                currentGameSave = JsonUtility.FromJson<GameSaveData>(saveFiles[0].text);
            }
        }
        return currentGameSave;
    }

    public SettingsSaveData GetSettinsSave()
    {
        if (currentSettingsSave == null)
        {
            currentSettingsSave = GetSettingsFile();
        }
        return currentSettingsSave;
    }

    private void GetAllSaveFile()
    {
        FileInfo[] files = new FileInfo[0];
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (!Directory.Exists(path))
        {
            Debug.Log("目前沒有任何存檔");
            return;
        }
        DirectoryInfo info = new DirectoryInfo(path);
        // 尋找檔名有Save但沒有Settings的json檔
        files = info.GetFiles("*.json")
                    .Where(file => file.Name.Contains("Save") && !file.Name.Contains("Settings"))
                    //聽AI說file.LastWriteTime包含路徑
                    .OrderByDescending(file => file.LastWriteTime)
                    .ToArray();

        foreach (var file in files)
        {
            string fileContent = File.ReadAllText(file.FullName);
            TextAsset textAsset = new TextAsset(fileContent);
            saveFiles.Add(textAsset);
        }
    }

    private SettingsSaveData GetSettingsFile()
    {
        IEnumerable<FileInfo> files = null;
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (Directory.Exists(path))
        {
            DirectoryInfo info = new DirectoryInfo(path);

            files = info.GetFiles("*.json")
                        .Where(file => file.Name.Contains("Settings"));
            //Debug.Log(files.Count());
            if (files.Count() <= 0)
            {
                CreateNewSettingsFile();
                return currentSettingsSave;
            }
        }
        if (files == null)
        {
            Debug.Log("files == null");
            CreateNewSettingsFile();
            return currentSettingsSave;
        }

        string fileContent = File.ReadAllText(files.First().FullName);
        currentSettingsSave = JsonUtility.FromJson<SettingsSaveData>(fileContent);
        return currentSettingsSave;
    }

    private void CreateNewSettingsFile()
    {
        Debug.Log("CreateNewSettingsFile");
        currentSettingsSave = new SettingsSaveData();
        string jsonString = JsonUtility.ToJson(currentSettingsSave);
        File.WriteAllText(path + "Settings" + filename, jsonString);
    }
}