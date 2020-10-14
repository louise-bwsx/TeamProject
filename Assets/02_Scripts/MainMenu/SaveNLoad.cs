using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNLoad: MonoBehaviour
{
    GetHitEffect getHitEffect;
    void Start()
    {
        getHitEffect = FindObjectOfType<GetHitEffect>();    
    }
    public void SaveData()
    {
        Debug.Log("進行存檔");
        CentralData.GetInst().dust = getHitEffect.dust;
        CentralData.SaveData();
    }
    public void LoadData()
    {
        Debug.Log("進行讀檔");
        getHitEffect.dust = CentralData.GetInst().dust;
        CentralData.LoadData();
    }
}
