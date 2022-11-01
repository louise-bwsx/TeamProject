using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SaveSpace : MonoBehaviour
{
    [SerializeField] private TMP_Text timeLabel;
    private Button saveBtn;

    private void Awake()
    {
        saveBtn = GetComponent<Button>();
    }

    private void Start()
    {
        saveBtn.onClick.AddListener(Save);
    }

    private void Save()
    {
        string date = DateTime.Now.ToString();
        timeLabel.text = date;
        SaveManager.Inst.SaveData(date);
    }
}