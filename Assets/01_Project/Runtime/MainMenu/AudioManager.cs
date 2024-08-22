using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource sourceBGM;
    [SerializeField] private AudioSource sourceSFX;
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipsDict = new Dictionary<string, AudioClip>();

    private const string MASTERVOLUME = "MasterVolume";
    private const string MUSICVOLUME = "MusicVolume";
    private const string SFXVOLUME = "SFXVolume";
    private const int MULITIPLIER = 30;

    protected override void OnAwake()
    {
        foreach (var clip in audioClips)
        {
            char[] splitChar = new char[] { '(', ')' };
            string[] clipName = clip.name.Split(splitChar);
            clipsDict.Add(clipName[1], clip);
        }
    }

    private void Start()
    {
        //放Awake太快
        Load();
    }

    private void Update()
    {
        if (!GameStateManager.Inst.IsGaming())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonUp(0))
        {
            PlaySFX("ButtonClick");
        }
    }

    public void StopBGM()
    {
        sourceBGM.Stop();
    }

    public void PlayBGM(string clipName)
    {
        AudioClip findClip = FIndClip(clipName);
        if (!findClip)
        {
            return;
        }
        if (sourceBGM.clip == findClip)
        {
            Debug.Log("不再重複播放相同的BGM: " + clipName);
            return;
        }
        sourceBGM.clip = findClip;
        sourceBGM.Play();
    }

    public void PlaySFX(string clipName)
    {
        AudioClip findClip = FIndClip(clipName);
        if (!findClip)
        {
            return;
        }
        sourceSFX.PlayOneShot(findClip);
    }

    private AudioClip FIndClip(string clipName)
    {
        if (!clipsDict.ContainsKey(clipName))
        {
            Debug.LogError("找不到此Audio: " + clipName);
            return null;
        }
        return clipsDict[clipName];
    }

    private void SetBGMVolume(float volume)
    {
        //Debug.Log(volume);
        SettingsSaveData settingsSave = SaveManager.Inst.GetSettinsSave();
        settingsSave.BGMVol = volume;
        audioMixer.SetFloat(MUSICVOLUME, Mathf.Log10(volume) * MULITIPLIER);
        SaveManager.Inst.SaveUserSettings();
    }

    private void SetSFXVolume(float volume)
    {
        SettingsSaveData settingsSave = SaveManager.Inst.GetSettinsSave();
        settingsSave.SFXVol = volume;
        audioMixer.SetFloat(SFXVOLUME, Mathf.Log10(volume) * MULITIPLIER);
        SaveManager.Inst.SaveUserSettings();
    }

    private void Load()
    {
        //Debug.Log("AudioManager.Load");
        SettingsSaveData settingsSave = SaveManager.Inst.GetSettinsSave();
        //0.05807604
        float bgmVolume = settingsSave.BGMVol;
        float sfxVolume = settingsSave.SFXVol;
        //Debug.Log(bgmVolume);
        audioMixer.SetFloat(MUSICVOLUME, Mathf.Log10(bgmVolume) * MULITIPLIER);
        audioMixer.SetFloat(SFXVOLUME, Mathf.Log10(sfxVolume) * MULITIPLIER);
        sliderBGM.value = bgmVolume;
        sliderSFX.value = sfxVolume;
        sliderBGM.onValueChanged.AddListener(SetBGMVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
    }
}