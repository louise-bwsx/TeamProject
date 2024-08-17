using UnityEngine;

public static class VectorExtensions
{
    public static Vector3 With(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static Vector3 Flat(this Vector3 original)
    {
        return new Vector3(original.x, 0, original.z);
    }

    /// <summary>
    /// 已經有Normalize
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
    {
        return (destination - source).normalized;
    }
}