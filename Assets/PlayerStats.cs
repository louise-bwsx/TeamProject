using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    void Start()
    {
        EquipmentManager.instance.onEquipmentChaged += OnEquipmentChanged;    
    }
    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        { 
        Armor.AddModifiers(newItem.armorModifier);
        Damage.AddModifiers(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            Armor.RemoveModifiers(oldItem.armorModifier);
            Damage.RemoveModifiers(oldItem.damageModifier);
        }
    }
}
