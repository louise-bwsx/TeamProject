using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSpace : MonoBehaviour
{
    [SerializeField] private TMP_Text timeLabel;
    private Button loadBtn;
    private string saveDate;

    private void Awake()
    {
        loadBtn = GetComponent<Button>();
    }

    private void Start()
    {
        loadBtn.onClick.AddListener(Load);
    }

    private void Load()
    {
        SaveManager.Inst.LoadData(saveDate);
    }
}