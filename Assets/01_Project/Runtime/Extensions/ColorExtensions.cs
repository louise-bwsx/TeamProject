using UnityEngine;

//TODO: 感覺可以合併到Helper
public static class ColorExtensions
{
    public static string ToHex(this Color source)
    {
        return "#" + ColorUtility.ToHtmlStringRGBA(source);
    }
}
