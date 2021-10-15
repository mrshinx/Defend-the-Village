using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Item",menuName = "Items/Default")]
public class DefaultItem : ItemObject {

    public void Awake()
    {
        itemType = ItemType.Default;
    }

}
