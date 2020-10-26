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
    public int[] charaterStats;
    public Text[] charaterNumber;

    void Start()
    {
        charaterNumber[(int)CharacterStats.STR].text = "" + charaterStats[(int)CharacterStats.STR];
        charaterNumber[(int)CharacterStats.DEF].text = "" + charaterStats[(int)CharacterStats.DEF];
        charaterNumber[(int)CharacterStats.AGI].text = "" + charaterStats[(int)CharacterStats.AGI];
        charaterNumber[(int)CharacterStats.INT].text = "" + charaterStats[(int)CharacterStats.INT];
        charaterNumber[(int)CharacterStats.SPR].text = "" + charaterStats[(int)CharacterStats.SPR];
    }
    public void StatsUpgrade(int charaterStats)
    {
        if (getHitEffect.dust >= SkillLevelneed)
        {
            getHitEffect.dust -= SkillLevelneed;
            //按鈕選擇的數值+=2
            this.charaterStats[charaterStats] += 2;
            //面板上的數值更改成實際的數值
            charaterNumber[charaterStats].text = "" + this.charaterStats[charaterStats];
        }
    }
}
