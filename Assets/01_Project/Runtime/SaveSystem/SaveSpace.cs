using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSpace : MonoBehaviour
{
    //TODOError: 這邊沒有 覆蓋 刪除 檔案會越來越多
    [SerializeField] private TMP_Text timeLabel;
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button deleteBtn;
    private FileInfo saveFile;

    private void Start()
    {
        saveBtn.onClick.AddListener(Save);
        deleteBtn.onClick.AddListener(Delete);
    }

    public void Init(FileInfo saveFile)
    {
        //Debug.Log("Init");
        this.saveFile = saveFile;
        deleteBtn.interactable = saveFile != null;
        if (saveFile == null)
        {
            timeLabel.text = "";
            return;
        }
        string jsonData = File.ReadAllText(saveFile.FullName);
        GameSaveData gameSave = JsonUtility.FromJson<GameSaveData>(jsonData);
        timeLabel.text = gameSave.time;
    }

    private void Save()
    {
        string date = DateTime.Now.ToString();
        timeLabel.text = date;
        saveFile = SaveManager.Inst.Save();
        Init(saveFile);
    }

    private void Delete()
    {
        if (saveFile == null)
        {
            Debug.LogWarning("無法刪除，因為 saveFile 為空");
            return;
        }
        string filePath = Path.Combine(saveFile.FullName);
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"檔案 {filePath} 不存在，無法刪除");
            return;
        }
        File.Delete(filePath);
        saveFile = null;
        Init(null);
        Debug.Log($"檔案 {filePath} 已刪除");
        SaveManager.Inst.RefreshUI();
    }
}