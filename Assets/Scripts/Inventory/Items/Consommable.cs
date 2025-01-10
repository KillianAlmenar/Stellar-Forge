using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consommable", menuName = "Item/Create New Consommable")]
public class Consommable : ItemInventory
{
    private new void OnValidate()
    {
        base.OnValidate();
        type = TYPE.CONSOMMABLE;
    }

    public void Use()
    {
        Debug.Log(itemName + " is used !");
    }
}
