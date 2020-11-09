using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject shadowDestory;
    public GameMenu gameMenu;

    public AudioSource BGMSource;
    public AudioSource SFXSource;//音效放置給所有怪物存取音效
    public AudioClip dieSFX;

    void Start()
    {
        gameMenu = FindObjectOfType<GameMenu>();
        SFXSource = GetComponentInParent<AudioSource>();
        SFXSource.volume = CentralData.GetInst().SFXVol;
    }
    public void Die()//動畫Event呼叫
    {
        shadowDestory.SetActive(false);
        BGMSource.clip = dieSFX;
        SFXSource.PlayOneShot(dieSFX);
        gameMenu.anyWindow[6].SetActive(true);
        Time.timeScale = 0;
    }
}
