using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemParent;
    public GameObject BackpackUI;
    Inventory inventory;
    InventorySlot[] inventorySlots;
    void Awake()
    {
        inventory = Inventory.instance;
        //將UpdateUI新增到同步呼叫的方法裡
        inventory.onItemChangedCallBack += UpdateUI;
        //取得所有塞InventorySlot的子物件
        inventorySlots = GetComponentsInChildren<InventorySlot>();
    }
    void Start()
    {
        //預設遊戲開啟時關閉 如果遊戲還沒開始時物件是關閉的會讀不到InventorySlot[]
        BackpackUI.SetActive(false);
    }
    void UpdateUI()
    {
        //Debug.Log(inventorySlots.Length);//這數字是gameObject InventorySlot
        //Debug.Log(inventory.Items.Count);//這數字是 每裝進背包一件物品加一  
        //最大值為 有塞InventorySlot子物件 的數量
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            //每撿到一次物品就會確認一次背包介面
            //每撿到一次物品inventory.Items.Count +1
            if (i < inventory.Items.Count)
            {
                inventorySlots[i].AddItem(inventory.Items[i]);
            }
            //如果i為1 inventory.Items.Count為0 則清空第i個的內容
            else
            {
                //暫時無作用 加了看不出效果
                inventorySlots[i].EmtyItem();
            }
        }
        Debug.Log("更新UI");
    }
}
