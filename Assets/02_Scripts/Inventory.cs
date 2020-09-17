using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region 獨體模式//看不懂的東西
    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("有一個以上的Inventory在遊戲中");
            return;
        }
        instance = this;
    }
    #endregion
    
    //看起來像是把其他script的方法存進來只要呼叫一次 就同步呼叫所有存進來的方法
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    
    //背包欄位
    public int space = 20;
    //宣告一個List儲存撿起來的東西
    public List<ItemScriptableObject> Items = new List<ItemScriptableObject>();
    //新增一個方法將撿起來的東西放進List
    public bool AddNewItem(ItemScriptableObject PickUpItem)
    {
        //不知道什麼意思
        if (!PickUpItem.isDefaultItem)
        {
            if (Items.Count>=space)
            {
                Debug.Log("空間不足");
                return false;
            }
            //成功才會把物品加進去List
            Items.Add(PickUpItem);
            //同步呼叫存在onItemChangedCallBack裡的方法
            onItemChangedCallBack.Invoke();
        }
        return true;//如果不加在這邊會報錯
    }
    //新增一個方法將List的東西刪除
    public void RemoveItem(ItemScriptableObject RemoveItem)
    {
        //將物品移除但不管按哪個格子的移除都是移除第一個
        Items.Remove(RemoveItem);
        //同步呼叫存在onItemChangedCallBack裡的方法
        onItemChangedCallBack.Invoke();
    }
}
