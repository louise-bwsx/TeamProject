using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEquipment",menuName ="Inventory/Equipment")]
//預設名稱 和 創建時的選單名稱 子選單名稱
public class Equipment : ItemScriptableObject
{
    //將裝備欄位數值化
    public EquipmentSlot equipmentSlot;

    public int armorModifier;//防禦力
    public int damageModifier;//攻擊力
    public override void Use()
    {
        base.Use();
        //裝備上去
        EquipmentManager.instance.Equip(this);
        //裝備消失
        RemoveFromInventory();
    }
}
//將裝備欄位數值化
public enum EquipmentSlot {
Head,
Chest,
Legs,
Weapon,
Sheild,
Feet}
