using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private PlayerStats getHitEffect;

    private void Awake()
    {
        getHitEffect = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        GetComponent<Text>().text = "剩餘魔塵:" + getHitEffect.dust + "\n";
    }
}
