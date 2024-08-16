using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoSingleton<AudioManager>
{
    //TODO: GameMenu的Slider好像不會跟著變
    [SerializeField] private AudioSource sourceBGM;
    [SerializeField] private AudioSource sourceSFX;
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    private Dictionary<string, AudioClip> clipsDict = new Dictionary<string, AudioClip>();

    public float volumeBGM => sliderBGM.value;
    public float volumeSFX => sliderSFX.value;
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
            //clipName[0] = 
            //clipName[1] = AfterBossFight
            //clipName[2] = 井の中の蛙
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
        if (GameStateManager.Inst.CurrentState == GameState.Gaming)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) ||
            Input.GetMouseButtonUp(0))
        {
            PlaySFX("ButtonClick");
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
    }

    public void PlaySFX(string clipName)
    {
        AudioClip findClip = FIndClip(clipName);
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
        Debug.Log(volume);
        CentralData.GetInst().BGMVol = volume;
        audioMixer.SetFloat(MUSICVOLUME, Mathf.Log10(volume) * MULITIPLIER);
        SaveManager.Inst.SaveUserSettings();
    }

    private void SetSFXVolume(float volume)
    {
        CentralData.GetInst().SFXVol = volume;
        audioMixer.SetFloat(SFXVOLUME, Mathf.Log10(volume) * MULITIPLIER);
        SaveManager.Inst.SaveUserSettings();
    }

    private void Load()
    {
        //0.05807604
        float bgmVolume = CentralData.GetInst().BGMVol;
        float sfxVolume = CentralData.GetInst().SFXVol;
        //Debug.Log(bgmVolume);
        audioMixer.SetFloat(MUSICVOLUME, Mathf.Log10(bgmVolume) * MULITIPLIER);
        audioMixer.SetFloat(SFXVOLUME, Mathf.Log10(sfxVolume) * MULITIPLIER);
        sliderBGM.value = bgmVolume;
        sliderSFX.value = sfxVolume;
        sliderBGM.onValueChanged.AddListener(SetBGMVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
    }
}