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
