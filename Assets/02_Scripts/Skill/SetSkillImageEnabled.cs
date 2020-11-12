using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillImageEnabled : MonoBehaviour
{
    public Image[] images;
    MobileSkillChoose mobileSkillChoose;
    void Start()
    {
        mobileSkillChoose = FindObjectOfType<MobileSkillChoose>();    
    }
    public void SetImageEnabled(bool enabled)
    {
        foreach (Image i in images)
        {
            i.enabled = enabled;
        }
        //當放開技能鍵時
        if (!enabled)
        {
            mobileSkillChoose.SkillShoot();
        }
    }
}
