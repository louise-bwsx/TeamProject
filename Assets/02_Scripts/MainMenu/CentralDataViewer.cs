using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralDataViewer : MonoBehaviour
{
    public float BGMVol
    {
        get
        {
            return CentralData.GetInst().BGMVol;
        }
    }
    public float SFXVol
    {
        get
        {
            return CentralData.GetInst().SFXVol;
        }
    }

}
