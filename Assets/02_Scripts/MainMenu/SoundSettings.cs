using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource SFX;
    public Slider BGMSlider;
    public Slider SFXSlider;

    void Awake()
    {
        //如果不這樣寫的話即使一開始聲音最大聲也可能預設值不同而聽不見
        BGM.volume = BGMSlider.value;
        CenterData.GetInstance().BGMVol = BGM.volume;
        SFX.volume = SFXSlider.value;
        CenterData.GetInstance().SFXVol = SFX.volume;
        Debug.Log(3);
    }
    public void BGMSoundSetting()
    {
        BGM.volume = BGMSlider.value;
        CenterData.GetInstance().BGMVol = BGM.volume;
    }
    public void SFXSoundSetting()
    {
        SFX.volume = SFXSlider.value;
        CenterData.GetInstance().SFXVol = SFX.volume;
    }
}
