using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public InventoryObject Inventory;

	void Start () {
		
	}
	
	void Update () {
		

    }

    private void OnApplicationQuit()
    {
        Inventory.Container.Clear();
    }
}
