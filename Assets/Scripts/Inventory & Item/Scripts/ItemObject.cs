using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Equipment,
    Consumable
}

public enum Attributes
{
    AttackDamage,
    AttackSpeed,
    ArmorPenetration,
    Mana,
    Knockback
}

public abstract class ItemObject : ScriptableObject {

    public int ID;
    public Sprite itemSprite;
    public ItemType itemType;
    [TextArea(15, 20)]
    public string description;
    public bool stackable;
    public int maxStack = 1;
    public ItemStat[] ItemStats;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

} 

[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
    public Sprite itemSprite;
    public bool stackable;
    public int maxStack = 1;
    public int itemLevel;
    public ItemStat[] ItemStats;

    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
        itemSprite = item.itemSprite;
        stackable = item.stackable;
        maxStack = item.maxStack;
        ItemStats = new ItemStat[item.ItemStats.Length];
        
        for (int i=0;i< ItemStats.Length;i++)
        {
            ItemStats[i] = new ItemStat(item.ItemStats[i].min, item.ItemStats[i].max, item.ItemStats[i].attribute);
        }

    }
    public Item(ItemObject item, int ilvl)
    {
        Name = item.name;
        ID = item.ID;
        itemSprite = item.itemSprite;
        stackable = item.stackable;
        maxStack = item.maxStack;
        itemLevel = ilvl;
        ItemStats = new ItemStat[item.ItemStats.Length];

        for (int i = 0; i < ItemStats.Length; i++)
        {
            ItemStats[i] = new ItemStat(item.ItemStats[i].min, item.ItemStats[i].max, item.ItemStats[i].attribute);
        }
    }
}

[System.Serializable]
public class ItemStat
{
    public Attributes attribute;
    public int ilvl;
    public int value;
    public int min;
    public int max;

    public ItemStat(int _min, int _max, int _ilvl, Attributes _attribute)
    {
        min = _min;
        max = _max;
        ilvl = _ilvl;
        attribute = _attribute;
        RollStats();
    }
    public ItemStat(int _min, int _max, Attributes _attribute)
    {
        min = _min;
        max = _max;
        attribute = _attribute;
        RollStats();
    }

    public void RollStats()
    {
        value = UnityEngine.Random.Range(min, max) * (1 + ilvl / 10);
    }
}
