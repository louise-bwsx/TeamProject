using UnityEngine;

//TODO: 感覺可以合併到Helper
public static class TransformExtensions
{
    /// <summary>
    /// 已經有Normalize
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static Vector3 DirectionTo(this Transform source, Transform destination)
    {
        return source.position.DirectionTo(destination.position);
    }

    /// <summary>
    /// 已經有Normalize
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector3 DirectionTo(this Transform source, Vector3 pos)
    {
        return source.position.DirectionTo(pos);
    }

    /// <summary>
    /// 刪除底下所有子物件
    /// </summary>
    /// <param name="source"></param>
    public static void DeleteAllChilldren(this Transform source)
    {
        while (source.childCount > 0)
        {
            Object.DestroyImmediate(source.GetChild(0).gameObject);
        }
    }
}