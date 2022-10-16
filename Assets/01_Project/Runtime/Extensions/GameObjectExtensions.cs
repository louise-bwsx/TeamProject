using UnityEngine;

//TODO: 感覺可以合併到Helper
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