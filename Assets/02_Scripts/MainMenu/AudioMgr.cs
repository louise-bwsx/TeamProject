using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMgr : MonoBehaviour
{
    public AudioSource BGMSource;
    public AudioSource SFXSource;
    public Slider BGMSlider;
    public Slider SFXSlider;
    void Start()
    {
        //音量初始
        BGMSource.volume = CentralData.GetInst().BGMVol;
        SFXSource.volume = CentralData.GetInst().SFXVol;
        BGMSlider.value = CentralData.GetInst().BGMVol;
        SFXSlider.value = CentralData.GetInst().SFXVol;
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
