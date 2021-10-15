using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour {

    public List<ItemObject> lootTable;
    public ItemObject drop;
    public int amount;
    InventoryManager masterInventory;

    private void Awake()
    {
        masterInventory = FindObjectOfType<InventoryManager>();
    }

    void OnMouseDown()
    {
        
        //Destroy(gameObject);
    }
}
