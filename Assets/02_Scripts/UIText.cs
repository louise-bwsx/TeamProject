using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    MobileStats getHitEffect;
    void Start()
    {
        getHitEffect = FindObjectOfType<MobileStats>();
    }
    void Update()
    {
        GetComponent<Text>().text = "剩餘魔塵:" + getHitEffect.dust + "\n";
    }
}
