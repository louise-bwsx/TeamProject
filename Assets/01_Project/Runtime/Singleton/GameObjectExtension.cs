using UnityEngine;
namespace NickABC.Utils.Extensions
{
    public static partial class ExtensionMethods
    {
        #region ObjectNull
        public static bool IsNull<T>(this T objects, string message = "") where T : class
        {
            if (objects is UnityEngine.Object obj)
            {
                if (!obj)
                {
                    if (message != "")
                        Debug.LogErrorFormat("The object is NULL! {0}", message);

                    return true;
                }
            }
            else
            {
                if (objects == null)
                {
                    if (message != "")
                        Debug.LogErrorFormat("The object is NULL! {0}", message);

                    return true;
                }
            }

            return false;
        }
        #endregion

        #region GameObject-GetOrAddComponent
        /// <summary>
        /// 如果有找到Component則回傳，反之則新增Component。
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject gameObjects) where T : Component
        {
            return gameObjects.GetComponent<T>() ?? gameObjects.AddComponent<T>();
        }
        #endregion
    }
}
