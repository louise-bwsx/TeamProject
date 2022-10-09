using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemScriptableObject itemScriptableObject;
    public void PickUp()
    {
        Debug.Log("玩家撿取: " + itemScriptableObject.name);
        //將是否加入背包的結果儲存
        bool wasPickUp = Inventory.instance.AddNewItem(itemScriptableObject);
        //消除地上的物品 不方便在AddNewItem裡面消除 讀不到gameObject
        if (wasPickUp == true)
        {
            Destroy(gameObject);
        }
    }
}
