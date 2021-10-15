using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootTable : MonoBehaviour {

    public ItemObject drop;
    public int amount;
    public int ilvl;
    InventoryManager masterInventory;

    private void Awake()
    {
        masterInventory = FindObjectOfType<InventoryManager>();
    }

    void OnMouseDown()
    {
        masterInventory.Inventory.AddItem(new Item(drop, ilvl), amount);
        //Destroy(gameObject);
    }

}
