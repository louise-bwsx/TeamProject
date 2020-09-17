using UnityEngine;

[CreateAssetMenu(fileName = "NewItem",menuName ="Inventory/Item")]
public class ItemScriptableObject : ScriptableObject
{
    //所有可以被撿起來的東西的成員 有點像建構子
    //又有點像一個自創空的Gameobject裡面包含以下東西

    //物品名 原先就有name所以前面加個new去覆蓋它
    new public string name = "NewItem";
    //物品圖示
    public Sprite icon = null;
    //不知道
    public bool isDefaultItem = false;
    //設定virtual 往後可以根據物品類別覆寫
    public virtual void Use()
    {
        Debug.Log("使用 " + name+ " 中");
    }
    public void RemoveFromInventory()
    {
        Inventory.instance.RemoveItem(this);
    }
}
