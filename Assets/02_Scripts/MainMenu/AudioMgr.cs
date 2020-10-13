using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;
    // Start is called before the first frame update
    void Start()
    {
        //source = this.GetComponent<AudioSource>();

        //音量初始
        BGMSource.volume = CentralData.GetInst().BGMVol;
        SFXSource.volume = CentralData.GetInst().SFXVol;
    }

    public float GetBGMVol()
    {
        return BGMSource.volume;
    }
    public float GetSFXVol()
    {
        return SFXSource.volume;
    }
    public void SetBGMVol (float v)
    {
        //呼叫CentralData的GetInst()為變數v
        CentralData.GetInst().BGMVol = v;
        BGMSource.volume= v;
    }
    public void SetSFXVol(float v)
    {
        CentralData.GetInst().SFXVol = v;
        SFXSource.volume = v;
    }

}
