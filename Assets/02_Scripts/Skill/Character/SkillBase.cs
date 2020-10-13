using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{

    public int SkillLevelneed = 1000;
    public int FireSkillLevel=0;
    public int PoisonSkillLevel = 0;
    public int SoilWallSkillLevel = 0;
    public int tornadoSkillLevel = 0;
    public int AirAttackSkillLevel = 0;
    public GetHitEffect getHitEffect;
    public Image[] FireImage;
    public Image[] PoisonImage;
    public Image[] SoilWallImage;
    public Image[] tornadoImage;
    public Image[] AirAttackImage;







    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
       
    
    }


    public void FireOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed*2;
            for (int i = 0; i < FireSkillLevel; i++)
            {
                FireImage[i].enabled = true;
            }
            FireSkillLevel++;


        }
    }
    public void PoisonOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed*2;
            for (int i = 0; i < PoisonSkillLevel; i++)
            {
                PoisonImage[i].enabled = true;
            }
            PoisonSkillLevel++;


        }
    }
    public void SoilWallOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed*2;
            for (int i = 0; i < SoilWallSkillLevel; i++)
            {
                SoilWallImage[i].enabled = true;
            }
            SoilWallSkillLevel++;


        }
    }
    public void tornadoOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed*2;
            for (int i = 0; i < tornadoSkillLevel; i++)
            {
                tornadoImage[i].enabled = true;
            }
            tornadoSkillLevel++;


        }
    }
    public void AirAttackOnClick()
    {
        if (getHitEffect.pickGold >= 0)
        {
            getHitEffect.pickGold -= SkillLevelneed*2;
            for (int i = 0; i < AirAttackSkillLevel; i++)
            {
                AirAttackImage[i].enabled = true;
            }
            AirAttackSkillLevel++;


        }
    }


}
