﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class AudioManager : MonoSingleton<AudioManager>, ILoadData
{
    [SerializeField] private AudioSource sourceBGM;
    [SerializeField] private AudioSource sourceSFX;
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private AudioMixer audioMixer; 
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();

    [field: SerializeField] public string currentPlayClipName;

        public float volumeBGM => sliderBGM.value;
    public float volumeSFX => sliderSFX.value;
    private const string MASTERVOLUME = "MasterVolume";
    private const string MUSICVOLUME = "MusicVolume";
    private const string SFXVOLUME = "SFXVolume";
    private const int MULITIPLIER = 30;

    private void Start()
    {
        sliderBGM.onValueChanged.AddListener(SetBGMVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonUp(0))
        {
            PlaySFX("Click");
        }
    }

    public void PlayBGM(string clipName)
    {
        AudioClip findClip = FIndClip(clipName);
        if (sourceBGM.clip == findClip)
        {
            Debug.Log("不再重複播放相同的BGM: " + clipName);
            return;
        }
        sourceBGM.clip = findClip;
        sourceBGM.Play();
        currentPlayClipName = clipName;
    }

    public void PlaySFX(string clipName)
    {
        AudioClip findClip = FIndClip(clipName);
        sourceSFX.PlayOneShot(findClip);
    }

        public void Load(object value1, object value2)

    {
        Debug.Log("設定音樂音效");
        SetBGMVolume((float)value1);
        SetSFXVolume((float)value2);
    }

    private AudioClip FIndClip(string clipName)
    {
        AudioClip findClip = clips.FirstOrDefault(clip => clip.name.Contains(clipName));
        if (findClip == null)
        {
            Debug.LogError("找不到此BGM: " + clipName);
        }
        return findClip;
    }

    private void SetBGMVolume(float volume)
    {
        CentralData.GetInst().BGMVol = volume;
        audioMixer.SetFloat(MUSICVOLUME, Mathf.Log10(volume) * MULITIPLIER);
        sliderBGM.value = volume;
        SaveManager.Inst.SaveUserSettings();
    }

    private void SetSFXVolume(float volume)
    {
        CentralData.GetInst().SFXVol = volume;
        audioMixer.SetFloat(SFXVOLUME, Mathf.Log10(volume) * MULITIPLIER);
        sliderSFX.value = volume;
        SaveManager.Inst.SaveUserSettings();
    }

    //[SerializeField] private EventTrigger[] eventTriggers;
    //[Button]
    //private void FindEventTrigger()
    //{
    //    eventTriggers = FindObjectsOfType<EventTrigger>();
    //}
}