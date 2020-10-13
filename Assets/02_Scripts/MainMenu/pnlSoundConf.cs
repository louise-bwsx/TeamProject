using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pnlSoundConf : MonoBehaviour
{
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioMgr audioMgr;
    void Start()
    {
        BGMSlider.value = audioMgr.GetBGMVol();
        SFXSlider.value = audioMgr.GetSFXVol();
    }
}
