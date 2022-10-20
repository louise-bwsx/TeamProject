using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Singleton
    public static ObjectPool Inst { get; private set; }
    private void Awake()
    {
        Inst = this;
    }
    #endregion
    [System.Serializable]
    public class Pool
    {
        public string name => prefab.name;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private Transform poolParent;
    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> poolDict = new Dictionary<string, Queue<GameObject>>();
    private void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject newObject = Instantiate(pool.prefab);
                newObject.transform.SetParent(poolParent);
                newObject.SetActive(false);
                objectPool.Enqueue(newObject);
            }
            poolDict.Add(pool.name, objectPool);
        }
    }

    public GameObject SpawnFromPool(string objectName, Vector3 pos, Quaternion rotation, Transform parent = null, float duration = 0)
    {
        if (!poolDict.ContainsKey(objectName))
        {
            Debug.LogError("物件池找不到: " + objectName);
            return null;
        }
        GameObject poolObject = poolDict[objectName].Dequeue();
        poolObject.SetActive(true);
        poolObject.transform.SetParent(parent);
        poolObject.transform.position = pos;
        poolObject.transform.rotation = rotation;
        StartCoroutine(DelayEnqueue(poolObject, duration));
        return poolObject;
    }

    public IEnumerator DelayEnqueue(GameObject poolObject, float duration)
    {
        if (duration != 0)
        {
            yield return new WaitForSeconds(duration);
        }
        string name = poolObject.name.Split('(')[0];
        poolObject.transform.SetParent(poolParent);
        poolDict[name].Enqueue(poolObject);
    }
}