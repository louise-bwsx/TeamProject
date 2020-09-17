using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region 獨體模式
    public static EquipmentManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChaged;

    //將 被裝備欄位 數值化
    Equipment[] currentEquipment;
    Inventory inventory;
    void Start()
    {
        inventory = Inventory.instance;
        //設定遊戲開始時玩家的裝備欄位有哪些 並 設定成數值
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        //將被裝備欄位設定為遊戲初始裝備欄位
        currentEquipment = new Equipment[numSlots];
    }
    //
    public void Equip(Equipment equipmentScriptable)
    {
        int slotIndex = (int)equipmentScriptable.equipmentSlot;
        Equipment oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddNewItem(oldItem);
        }
        //onEquipmentChaged.Invoke(equipmentScriptable, oldItem);
        currentEquipment[slotIndex] = equipmentScriptable;
    }
    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.AddNewItem(oldItem);
            currentEquipment[slotIndex] = null;

            //onEquipmentChaged.Invoke(null, oldItem);
        }
    }
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
