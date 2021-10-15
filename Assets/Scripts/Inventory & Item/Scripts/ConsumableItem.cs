using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable")]
public class ConsumableItem : ItemObject
{

    public void Awake()
    {
        itemType = ItemType.Consumable;
    }

}
