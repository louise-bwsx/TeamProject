using UnityEngine;

public static class GameObjectExtensions
{
    public static void DestroyAllChildren(this GameObject source)
    {
        foreach (Transform childObject in source.transform)
        {
            Object.Destroy(childObject.gameObject);
        }
    }
}