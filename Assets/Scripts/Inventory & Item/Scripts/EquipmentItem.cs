using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Items/Equipment")]
public class EquipmentItem : ItemObject
{


    public void Awake()
    {
        itemType = ItemType.Equipment;
        stackable = false;
    }

    
}
