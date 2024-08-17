using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingleton<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public string name => prefab.name;
        public GameObject prefab;
        public int defaultSize = 5;
    }

    [SerializeField] private Transform poolParent;
    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();

    private void Start()
    {
        Initialize();
    }

    public GameObject SpawnFromPool(string objectName, Vector3 pos, Quaternion rotation, Transform parent = null, float duration = -1)
    {
        if (!FindObjectInPoolByName(objectName))
        {
            return null;
        }
        GameObject poolObject = poolDict[objectName].Dequeue();
        if (poolDict[objectName].Count < 1)
        {
            Debug.Log(objectName + "數量不夠1補生成");
            InstatiateObject(poolObject);
        }
        poolObject.SetActive(true);
        poolObject.transform.SetParent(parent);
        poolObject.transform.position = pos;
        poolObject.transform.rotation = rotation;
        StartCoroutine(DelayEnqueueCoroutine(poolObject, duration));
        return poolObject;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="poolObject">需要回收的物件</param>
    /// <param name="duration">-1: 不需要回收至物件池<para></para>
    ///                         0: 立即回收<para></para>
    ///                        >0: 等待數秒再回收<para></para></param>
    public void PutBackInPool(GameObject poolObject, float duration)
    {
        StartCoroutine(DelayEnqueueCoroutine(poolObject, duration));
    }

    private IEnumerator DelayEnqueueCoroutine(GameObject poolObject, float duration)
    {
        string name = poolObject.name.Split('(')[0];
        if (duration == -1)
        {
            Debug.Log(name + " 不需要回收至物件池");
            Destroy(poolObject);
            yield break;
        }
        if (duration > 0)
        {
            yield return new WaitForSeconds(duration);
        }
        if (!poolObject)
        {
            Debug.Log(name + "再等待數秒後 被提早收回");
            yield break;
        }
        if (!FindObjectInPoolByName(name))
        {
            Destroy(poolObject);
            yield break;
        }
        //Debug.Log(name + ": 物件回收至物件池");
        poolObject.transform.SetParent(poolParent);
        poolObject.SetActive(false);
        poolDict[name].Enqueue(poolObject);
    }

    private bool FindObjectInPoolByName(string objectName)
    {
        if (!poolDict.ContainsKey(objectName))
        {
            Debug.LogError("物件池找不到: " + objectName);
            return false;
        }
        return true;
    }

    private void InstatiateObject(GameObject poolObject)
    {
        GameObject newObject = Instantiate(poolObject);
        newObject.transform.SetParent(poolParent);
        newObject.SetActive(false);
        newObject.name = newObject.name.Split('(')[0];
        poolDict[poolObject.name].Enqueue(newObject);
    }

    private void Initialize()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            if (poolDict.ContainsKey(pool.name))
            {
                continue;
            }
            poolDict.Add(pool.name, objectPool);
            for (int i = 0; i < pool.defaultSize; i++)
            {
                InstatiateObject(pool.prefab);
            }
        }
    }
}