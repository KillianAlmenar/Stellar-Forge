using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New KeyItem", menuName = "Item/Create New KeyItem")]
public class KeyItem : ItemInventory
{
    private new void OnValidate()
    {
        base.OnValidate();
        type = TYPE.KEYITEM;
    }
}
