using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveManager : MonoSingleton<SaveManager>
{
    private GameSaveData currentGameSave;
    private SettingsSaveData currentSettingsSave;
    private static string path = Application.dataPath + "/Playerdata/";
    private static string filename = "Save.json";
    [SerializeField, ReadOnly] private LoadSpace[] loadSpaces;
    [SerializeField, ReadOnly] private SaveSpace[] saveSpaces;
    [SerializeField, ReadOnly] private List<FileInfo> saveFiles = new List<FileInfo>();
    private int saveIndex = 1;

    public List<ISave> ISaves { get; private set; } = new List<ISave>();//interface沒辦法SerializeField

    protected override void OnAwake()
    {
        Debug.Log("SaveManager.OnAwake()");
        loadSpaces = GetComponentsInChildren<LoadSpace>(true);
        saveSpaces = GetComponentsInChildren<SaveSpace>(true);
        RefreshUI();
    }

    public void RefreshUI()
    {
        GetAllSaveFile();
        //Debug.Log(saveFiles.Count);
        for (int i = 0; i < loadSpaces.Length; i++)
        {
            if (saveFiles.Count == 0 || i > saveFiles.Count - 1)
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

    public FileInfo Save()
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
        //Debug.Log($"jsonString: {jsonString}");
        //Debug.Log($"currentGameSave: {currentGameSave}");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        // 生成初始的完整文件路径
        string fullPath = Path.Combine(path, saveIndex + filename);

        // 如果文件名已存在，递增 saveIndex，直到找到一个不重复的文件名
        while (File.Exists(fullPath))
        {
            saveIndex++;
            fullPath = Path.Combine(path, saveIndex + filename);
        }

        // 保存文件
        File.WriteAllText(fullPath, jsonString);
        Debug.Log("存檔成功");

        return new FileInfo(fullPath);
    }

    public void Load(FileInfo saveFile)
    {
        if (!File.Exists(path + filename))
        {
            Debug.LogError($"!File.Exists Path:{path} FileName:{filename}");
            return;
        }

        string jsonData = File.ReadAllText(saveFile.FullName);
        currentGameSave = JsonUtility.FromJson<GameSaveData>(jsonData);
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
                string jsonData = File.ReadAllText(saveFiles[0].FullName);
                currentGameSave = JsonUtility.FromJson<GameSaveData>(jsonData);
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

    public string GetSaveFilePath()
    {
        return path;
    }

    private void GetAllSaveFile()
    {
        //為了不每次GetAll時越來越多
        saveFiles.Clear();
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
            saveFiles.Add(file);
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