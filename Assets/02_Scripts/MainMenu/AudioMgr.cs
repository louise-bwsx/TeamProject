using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMgr : MonoBehaviour
{
    public AudioSource BGMSource;
    //public AudioSource[] SFXSource;//要取所有怪物 和玩家的AudioSource
    public GameObject[] SFXGameObjectGroup1;
    public GameObject[] SFXGameObjectGroup2;
    public Slider BGMSlider;
    public Slider SFXSlider;
    public AudioClip beforeBGM;
    public AudioClip afterBGM;
    public BossHealth bossHealth;
    public CloseDoor closeDoor;
    public GetHitEffect getHitEffect;
    void Start()
    {
        //好用的東東但用不到 會取所有的AudioSource 連帶影響到BGM
        //SFXSource = FindObjectsOfType(typeof(AudioSource))as AudioSource[];
        //抓取全部有以下 Tag的物件
        SFXGameObjectGroup1 = GameObject.FindGameObjectsWithTag("Monster");
        SFXGameObjectGroup2 = GameObject.FindGameObjectsWithTag("SFXSource");//本來只有一個現在Player跟Canvas
        getHitEffect = FindObjectOfType<GetHitEffect>();
        BGMSource = GetComponent<AudioSource>();
        //音量初始
        //foreach (AudioSource SFXAudioSource in SFXSource)
        //{
        //    SFXAudioSource.volume = CentralData.GetInst().SFXVol;
        //}
        //所有有Monster Tag的物件的音量全部設定
        foreach (GameObject SFXAudioSource in SFXGameObjectGroup1)
        {
            SFXAudioSource.GetComponent<AudioSource>().volume = CentralData.GetInst().SFXVol;
        }
        foreach (GameObject SFXAudioSource in SFXGameObjectGroup2)
        {
            SFXAudioSource.GetComponent<AudioSource>().volume = CentralData.GetInst().SFXVol;
        }

        BGMSource.volume = CentralData.GetInst().BGMVol;
        BGMSlider.value = CentralData.GetInst().BGMVol;
        SFXSlider.value = CentralData.GetInst().SFXVol;
    }
    void Update()
    {
        if (bossHealth !=null && bossHealth.Hp <= 0)
        {
            if (BGMSource.clip != afterBGM)
            {
                BGMSource.Stop();
                BGMSource.clip = afterBGM;
                BGMSource.Play();
            }
        }
        else if (closeDoor !=null && closeDoor.aa.activeSelf && getHitEffect.playerHealth>0)
        {
            if (BGMSource.clip != beforeBGM)
            {
                BGMSource.clip = beforeBGM;
                BGMSource.Play();
            }
        }

    }
    public void SetBGMVol (float v)
    {
        //呼叫CentralData的GetInst()為變數v
        //CentralData.GetInst().BGMVol = v;

        BGMSource.volume= v;
    }
    public void SetSFXVol(float v)
    {
        //CentralData.GetInst().SFXVol = v;
        //foreach (AudioSource audioSource in SFXSource)
        //{
        //    audioSource.volume = v;
        //}

        foreach(GameObject SFXAudioSource in SFXGameObjectGroup1)
        {
            SFXAudioSource.GetComponent<AudioSource>().volume = v;
        }
        foreach (GameObject SFXAudioSource in SFXGameObjectGroup2)
        {
            SFXAudioSource.GetComponent<AudioSource>().volume = v;
        }
    }
}
