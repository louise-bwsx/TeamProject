using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpace : MonoBehaviour
{
    [SerializeField] private TMP_Text timeLabel;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button deleteBtn;
    private FileInfo saveFile;

    private void Start()
    {
        loadBtn.onClick.AddListener(Load);
        deleteBtn.onClick.AddListener(Delete);
    }

    public void Init(FileInfo saveFile)
    {
        this.saveFile = saveFile;
        loadBtn.interactable = saveFile != null;
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

    private void Load()
    {
        if (saveFile == null)
        {
            Debug.Log("沒有存檔");
            return;
        }
        SaveManager.Inst.Load(saveFile);
        SceneManager.Inst.LoadLevel("GameScene");
    }

    private void Delete()
    {
        if (saveFile == null)
        {
            Debug.LogWarning("無法刪除，因為 saveFile 為空");
            return;
        }
        Debug.Log(saveFile.FullName);
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