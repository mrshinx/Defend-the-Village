using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName ="New Inventory", menuName = "Inventory")]
public class InventoryObject : ScriptableObject  {

    public List<InventorySlot> Container = new List<InventorySlot>();
    public ItemDatabaseObject database;
    public string savePath;

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Item Database.asset", typeof(ItemDatabaseObject));
#else   
        database = Resources.Load<ItemDatabaseObject>("Item Database");
#endif
    }

    public void AddItem(Item _item, int _amount)
    {
        if (_item.stackable)
        {
            for (int i = 0; i < Container.Count; i++)
            {
                if ((Container[i].item.ID == _item.ID)&&(Container[i].amount != _item.maxStack))
                {
                    if (Container[i].amount + _amount <= _item.maxStack)
                    {
                        Container[i].AddAmount(_amount);
                        return;
                    }
                    else 
                    {
                        Container[i].AddAmount(_item.maxStack - Container[i].amount);
                        Container.Add(new InventorySlot(_item.ID, _item, _amount - (_item.maxStack - Container[i].amount)));
                        return;
                    }
                }
            }
        }
        
            Container.Add(new InventorySlot(_item.ID, _item, _amount));
    }


}



[System.Serializable]
public class InventorySlot  
{
    public Item item;
    public int amount;
    public int ID = -1;

    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}


