using NickABC.Utils.Extensions;
using System;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 為了不要讓編輯器停止時在OnDestroy裡呼叫Singleton 導致Error
    /// </summary>
    public static bool applicationQuit = false;

    #region Singleton Logic
    public static T Inst
    {
        get
        {
            lock (locks)
            {
                if (inst == null)
                {
                    inst = (T)FindObjectOfType(typeof(T));

                    if (inst == null && !applicationQuit)
                    {
                        Create();   //創建新的物件
                    }
                }
            }
            return inst;
        }
    }
    private static T inst;
    /// <summary>
    /// Prefab素材路徑
    /// </summary>
    protected static string assetPath = "Prefab/Singleton/";
    private static object locks = new object();

    private static void Create()
    {
        try
        {
            var singletonPrefab = Resources.Load(assetPath + typeof(T));
            GameObject singletonObj;

            if (singletonPrefab.IsNull())
            {
                singletonObj = new GameObject();    //已有Prefab
            }
            else
            {
                singletonObj = (GameObject)Instantiate(singletonPrefab);    //已有Prefab
            }
            singletonObj.name = typeof(T).ToString() + " (Singleton)";  //改名子
            inst = singletonObj.GetOrAddComponent<T>();   //如不方便放腳本 or 忘記放腳本到Prefab時，實例化後再掛載上去。
        }
        catch (ArgumentException i_ex)
        {
            Debug.LogError(string.Format("{0}: {1}", i_ex.GetType().Name, i_ex.Message));
        }
    }
    /// <summary>
    /// 設定Prefab素材路徑
    /// </summary>
    protected virtual string GetAssetPath()
    {
        return "Prefab/Singleton/";
    }
    #endregion
    #region Manadatory Implementation
    /// <summary>
    /// 取得Singleton 名子
    /// </summary>
    protected virtual string GetSingletonName()
    {
        return gameObject.name;
    }
    /// <summary>
    /// 是否過場不刪除
    /// </summary>
    protected virtual bool IsDestroyOnLoad()
    {
        return false;
    }
    #endregion
    #region Initialization Logic
    private void Awake()
    {
        if (Inst != this)
        {
            Debug.LogWarning(string.Format("{0}{1}", typeof(T), " will destroy the extra gameObject!!"));
            Destroy(gameObject);
            return;
        }
        if (IsDestroyOnLoad() == false)
        {
            DontDestroyOnLoad(gameObject);
        }
        OnAwake();
    }
    protected virtual void OnAwake()
    {
    }
    protected virtual void OnDestroy()
    {
        inst = null;
    }
    #endregion

    private void OnApplicationQuit()
    {
        applicationQuit = true;
    }
}
