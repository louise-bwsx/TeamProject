using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShine : MonoBehaviour
{
    public Text text;
    public Image image;
    void Update()
    {
        Color alpha = Color.clear;
        Color noAlpha = Color.white;
        //好像變太快顯示不出來
        if (image.color.a >= 0)
        {
            Debug.Log(image.color);
            //變換前，變換後
            image.color = Color.Lerp(alpha, noAlpha, Time.unscaledDeltaTime);
        }
        else if (image.color.a < 1)
        {
            //Debug.Log(image.color);
            image.color = Color.Lerp(noAlpha, alpha, Time.unscaledDeltaTime);
        }
    }
}
