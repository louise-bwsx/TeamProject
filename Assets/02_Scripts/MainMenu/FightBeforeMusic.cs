using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBeforeMusic : MonoBehaviour
{
  AudioSource audioSource;
    public AudioClip fightingBgm;
    public AudioClip fightAfterBgm;
    public BossFightRule bossFightRule;
    public AudioMgr audioMgr;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = CentralData.GetInst().BGMVol;
        audioMgr = FindObjectOfType<AudioMgr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (bossFightRule.bossFightState <= 3)
            {
                //audioMgr.BGMSource.Stop;
                audioMgr.BGMSource.enabled = false;
                audioSource.clip = fightingBgm;
                audioSource.PlayOneShot(fightingBgm);
                //audioSource.clip = fightBgm;
            }
            else if (bossFightRule.bossFightState > 3)
            {
                audioSource.clip = fightAfterBgm;
                audioSource.PlayOneShot(fightAfterBgm);
            }
        }
        //return true;
    }
}
