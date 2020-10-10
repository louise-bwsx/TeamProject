using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class pnlSoundConf : MonoBehaviour
{
    public Slider slider = null;
    public AudioMgr audioMgr = null;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = audioMgr.GetVol();
    }
    public void CloseDlg()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
        void Update()
    {
        
    }
}
