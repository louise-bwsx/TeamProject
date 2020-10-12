using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBase : MonoBehaviour
{
    public float cube = 0;
    public int SkillLevelneed = 100;
    public GetHitEffect getHitEffect;
    public int STR = 0;
    public int DEF = 0;
    public int AGI = 0;
    public int INT = 0;
    public int SPR = 0;





    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }


    public void StrOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed;
            STR += 2;
           
        }
    }
    public void DefOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed;
            DEF += 2;

        }
    }
    public void AgiOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed;
            AGI += 2;

        }
    }
    public void IntOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed;
            INT += 2;

        }
    }
    public void SprOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed;
            SPR += 2;

        }
    }
}
