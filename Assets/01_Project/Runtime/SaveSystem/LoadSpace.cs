using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSpace : MonoBehaviour
{
    //TODOError: 還沒確認這邊有沒有正常讀取
    [SerializeField] private TMP_Text timeLabel;
    private Button loadBtn;
    private TextAsset saveFile;

    private void Awake()
    {
        loadBtn = GetComponent<Button>();
    }

    private void Start()
    {
        loadBtn.onClick.AddListener(Load);
    }

    public void Init(TextAsset saveFile)
    {
        this.saveFile = saveFile;
        if (saveFile == null)
        {
            timeLabel.text = "";
            return;
        }
        GameSaveData gameSave = JsonUtility.FromJson<GameSaveData>(saveFile.text);
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
    }
}