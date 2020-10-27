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
    SPR,
    Count
}

public class CharacterBase : MonoBehaviour
{
    public int SkillLevelneed = 100;
    public GetHitEffect getHitEffect;
    public int[] charaterStats;
    public Text[] charaterNumber;

    void Start()
    {
        for (int i = 0; i < (int)CharacterStats.Count; i++)
        {
            charaterStats[i] = CentralData.GetInst().charaterStats[i];
        }
        for (int i = 0; i < charaterStats.Length; i++)
        {
            charaterNumber[i].text = charaterStats[i].ToString();
        }
        //charaterNumber[(int)CharacterStats.STR].text = "" + charaterStats[(int)CharacterStats.STR];
        //charaterNumber[(int)CharacterStats.DEF].text = "" + charaterStats[(int)CharacterStats.DEF];
        //charaterNumber[(int)CharacterStats.AGI].text = "" + charaterStats[(int)CharacterStats.AGI];
        //charaterNumber[(int)CharacterStats.INT].text = "" + charaterStats[(int)CharacterStats.INT];
        //charaterNumber[(int)CharacterStats.SPR].text = "" + charaterStats[(int)CharacterStats.SPR];
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
