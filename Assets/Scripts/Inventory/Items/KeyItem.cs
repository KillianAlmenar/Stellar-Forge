using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New KeyItem", menuName = "Item/Create New KeyItem")]
public class KeyItem : Item
{
    private void OnValidate()
    {
        type = TYPE.KEYITEM;
    }
}
