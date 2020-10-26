using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterStats
{ 
    STR,
    DEF,
    AGI,
    INT,
    SPR
}

public class CharacterBase : MonoBehaviour
{
    public int SkillLevelneed = 100;
    public GetHitEffect getHitEffect;
    public int STR;
    public int DEF;
    public int AGI;
    public int INT;
    public int SPR;

    public Text[] charaterStats;

    void Start()
    {
        charaterStats[(int)CharacterStats.STR].text = "" + STR;
        charaterStats[(int)CharacterStats.DEF].text = "" + DEF;
        charaterStats[(int)CharacterStats.AGI].text = "" + AGI;
        charaterStats[(int)CharacterStats.INT].text = "" + INT;
        charaterStats[(int)CharacterStats.SPR].text = "" + SPR;
    }

    public void StrOnClick()
    {
        if (getHitEffect.dust >= 0)
        {
            getHitEffect.dust -= SkillLevelneed;
            STR += 2;
        }
    }
    public void DefOnClick()
    {
        if (getHitEffect.dust >= 0)
        {
            getHitEffect.dust -= SkillLevelneed;
            DEF += 2;
        }
    }
    public void AgiOnClick()
    {
        if (getHitEffect.dust >= 0)
        {
            getHitEffect.dust -= SkillLevelneed;
            AGI += 2;
        }
    }
    public void IntOnClick()
    {
        if (getHitEffect.dust >= 0)
        {
            getHitEffect.dust -= SkillLevelneed;
            INT += 2;
        }
    }
    public void SprOnClick()
    {
        if (getHitEffect.dust >= 0)
        {
            getHitEffect.dust -= SkillLevelneed;
            SPR += 2;
        }
    }
}
