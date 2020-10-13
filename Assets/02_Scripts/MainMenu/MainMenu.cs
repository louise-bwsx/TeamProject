using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void SaveData()
    {
        Debug.Log("進行存檔");
        CentralData.SaveData();
    }
}
