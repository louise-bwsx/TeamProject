using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterData : MonoBehaviour
{
    static CenterData mInstace = null;
    public static CenterData GetInstance()
    {
        if (mInstace == null)
        {
            mInstace = new CenterData();
        }
        return mInstace;
    }
    public float BGMVol;
    public float SFXVol;
    private CenterData()
    {

    }
}
