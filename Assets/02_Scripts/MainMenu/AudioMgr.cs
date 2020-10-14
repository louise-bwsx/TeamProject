using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMgr : MonoBehaviour
{
    AudioSource BGMSource;
    public AudioSource[] SFXSource;//要取所有怪物 和玩家的AudioSource
    public Slider BGMSlider;
    public Slider SFXSlider;
    void Start()
    {
        //好用的東東但用不到
        //SFXSource = FindObjectsOfType(typeof(AudioSource))as AudioSource[];
        BGMSource = GetComponent<AudioSource>();
        //音量初始
        foreach (AudioSource SFXAudioSource in SFXSource)
        {
            SFXAudioSource.volume = CentralData.GetInst().SFXVol;
        }
        BGMSource.volume = CentralData.GetInst().BGMVol;
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
        foreach (AudioSource audioSource in SFXSource)
        {
            audioSource.volume = v;
        }
        //SFXSource.volume = v;
    }

}
