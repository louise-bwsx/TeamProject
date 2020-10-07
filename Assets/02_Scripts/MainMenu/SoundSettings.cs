using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundSettings : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource SFX;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public GameObject gamemanager;

    //待更改 他一直跳出黃色的東東很煩應該是沒辦法存取上次設定的關係
    //void Awake()
    //{
    //    if (gamemanager != null)//條件改成在menu才設定
    //    {
    //        BGMSlider.value = CenterData.GetInstance().BGMVol;
    //        SFXSlider.value = CenterData.GetInstance().SFXVol;
    //    }
    //    //如果不這樣寫的話即使一開始聲音最大聲也可能預設值不同而聽不見
    //    BGM.volume = BGMSlider.value;
    //    BGM.volume = CenterData.GetInstance().BGMVol;
    //    SFX.volume = SFXSlider.value;
    //    SFX.volume = CenterData.GetInstance().SFXVol;
        
    //}
    //public void BGMSoundSetting()
    //{
    //    BGM.volume = BGMSlider.value;
    //    CenterData.GetInstance().BGMVol = BGM.volume;
    //}
    //public void SFXSoundSetting()
    //{
    //    SFX.volume = SFXSlider.value;
    //    CenterData.GetInstance().SFXVol = SFX.volume;
    //}
}
