using UnityEngine;

public static class ColorExtensions
{
    public static string ToHex(this Color source)
    {
        return "#" + ColorUtility.ToHtmlStringRGBA(source);
    }
}