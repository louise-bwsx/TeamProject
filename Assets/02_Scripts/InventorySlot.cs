using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button RemoveButton;

    ItemScriptableObject itemScriptableObject;

    //將物品的icon和移除按鈕顯示在背包介面上的方法
    public void AddItem(ItemScriptableObject newItem)
    {
        itemScriptableObject = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
        RemoveButton.interactable = true;
    }
    public void EmtyItem()
    {
        icon.sprite = null;
        icon.enabled = false;
        itemScriptableObject = null;
        RemoveButton.interactable = false;
    }
    public void RemoveItem()
    {
        //將物品移除但不管按哪個格子的移除都是移除第一個
        Inventory.instance.RemoveItem(itemScriptableObject);
    }
    public void UseItem()
    {
        if (itemScriptableObject != null)
        {
            itemScriptableObject.Use();
        }
    }
}
