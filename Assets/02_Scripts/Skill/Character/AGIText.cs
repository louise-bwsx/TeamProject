using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AGIText : MonoBehaviour
{
    //public MonsterSpwan MonsterSpwan;
    //public GetHitEffect getHitEffect;
    //public Inventory inventory;
    public CharacterBase characterBase;

    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Text>().text = characterBase.AGI + "\n" ;
            /*"血量：　　　" + getHitEffect.playerHealth + "\n" +*/

        //"剩餘敵人：　" + MonsterSpwan.monstervalue + "\n";
    }

}
