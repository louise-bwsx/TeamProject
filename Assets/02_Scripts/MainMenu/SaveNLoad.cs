using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad: MonoBehaviour
{
    public void SaveData()
    {
        Debug.Log("進行存檔");
        CentralData.SaveData();
    }
    public void LoadData()
    {
        Debug.Log("進行讀檔");
        CentralData.LoadData();
    }
}
