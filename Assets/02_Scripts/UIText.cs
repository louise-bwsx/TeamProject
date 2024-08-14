using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private MobileStats getHitEffect;

    private void Awake()
    {
        getHitEffect = FindObjectOfType<MobileStats>();
    }

    private void Update()
    {
        GetComponent<Text>().text = "剩餘魔塵:" + getHitEffect.dust + "\n";
    }
}
