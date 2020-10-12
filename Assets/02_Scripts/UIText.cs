using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    public MonsterSpwan MonsterSpwan;
    public GetHitEffect getHitEffect;
    void Start()
    {
        
    }

    void Update()
    {
        GetComponent<Text>().text = "剩餘魔塵：" + getHitEffect.pickGold + "\n" ;
            /*"血量：　　　" + getHitEffect.playerHealth + "\n" +*/

        //"剩餘敵人：　" + MonsterSpwan.monstervalue + "\n";
    }

}
