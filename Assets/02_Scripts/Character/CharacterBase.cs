using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    public int SkillLevelneed = 100;
    public GetHitEffect getHitEffect;
    public int STR = 0;
    public int DEF = 0;
    public int AGI = 0;
    public int INT = 0;
    public int SPR = 0;

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
