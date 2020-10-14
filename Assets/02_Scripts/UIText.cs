using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    GetHitEffect getHitEffect;
    void Start()
    {
        getHitEffect = FindObjectOfType<GetHitEffect>();    
    }
    void Update()
    {
        GetComponent<Text>().text = "剩餘魔塵:" + getHitEffect.pickGold + "\n";
    }
}
